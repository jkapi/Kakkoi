using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Lidgren.Network;
using System.IO;
using System.Net.Http;

namespace StrangerCade.Framework.Multiplayer
{
    class SocketHandler
    {
        static Object StateLock = new Object();
        static Object DataLock = new Object();
        static bool IsRunning = true;
        static NetClient client;
        static List<Player> GameStateList;
        static MemoryStream GameData = null;
        public static bool Connected { get {
                if (client != null) return (client.ConnectionStatus == NetConnectionStatus.Connected);
                else return false; } }
        static string Sessid = "";
        public static string PlayerName;
        public static int UserId;
        static Dictionary<PacketTypes, Action<NetIncomingMessage>> handlers = new Dictionary<PacketTypes, Action<NetIncomingMessage>>();

        public static HttpClient HttpClient = new HttpClient();
        public static void Connect(string sessid, string ip)
        {
            Sessid = sessid;

            NetPeerConfiguration config = new NetPeerConfiguration("Kakoi");
            client = new NetClient(config);
            NetOutgoingMessage outmsg = client.CreateMessage();
            PlayerName = sessid;

            client.Start();

            outmsg.Write((short)PacketTypes.LOGINSESSID);
            outmsg.Write(sessid);

            client.Connect(ip, 25000, outmsg);

            GameStateList = new List<Player>();

            new Thread(IncomingHandler).Start();
        }

        public static void SendMessage(PacketTypes type, params byte[] data)
        {
            NetOutgoingMessage outmsg = client.CreateMessage();
            outmsg.Write((short)type);
            outmsg.Write((byte)DataTypes.BYTE);
            outmsg.Write((int)data.Length);
            foreach (var obj in data)
            {
                outmsg.Write(obj);
            }
            client.SendMessage(outmsg, NetDeliveryMethod.ReliableOrdered);
        }

        internal static List<Player> GetPlayers()
        {
            lock (StateLock)
            {
                return new List<Player>(GameStateList);
            }
        }

        internal static BinaryReader GetData()
        {
            lock (DataLock)
            {
                if (GameData != null)
                {
                    GameData.Position = 0;
                    MemoryStream temp = new MemoryStream((int)GameData.Length);
                    GameData.CopyTo(temp);
                    return new BinaryReader(temp);
                }
                else
                {
                    return null;
                }
            }
        }

        public static void SendMessage(PacketTypes type, params float[] data)
        {
            if (Connected)
            {
                NetOutgoingMessage outmsg = client.CreateMessage();
                outmsg.Write((short)type);
                outmsg.Write((byte)DataTypes.FLOAT);
                outmsg.Write((int)data.Length);
                foreach (var obj in data)
                {
                    outmsg.Write(obj);
                }
                client.SendMessage(outmsg, NetDeliveryMethod.ReliableOrdered);
            }
        }

        public static void SendMessage(PacketTypes type, params string[] data)
        {
            if (Connected)
            {
                NetOutgoingMessage outmsg = client.CreateMessage();
                outmsg.Write((short)type);
                outmsg.Write((byte)DataTypes.STRING);
                outmsg.Write((int)data.Length);
                foreach (var obj in data)
                {
                    outmsg.Write(obj);
                }
                client.SendMessage(outmsg, NetDeliveryMethod.ReliableOrdered);
            }
        }

        public static void SendMessage(PacketTypes type, params int[] data)
        {
            if (Connected)
            {
                NetOutgoingMessage outmsg = client.CreateMessage();
                outmsg.Write((short)type);
                outmsg.Write((byte)DataTypes.INT);
                outmsg.Write((int)data.Length);
                foreach (var obj in data)
                {
                    outmsg.Write(obj);
                }
                client.SendMessage(outmsg, NetDeliveryMethod.ReliableOrdered);
            }
        }

        public static void SendMessage(PacketTypes type)
        {
            if (Connected)
            {
                NetOutgoingMessage outmsg = client.CreateMessage();
                outmsg.Write((short)type);
                outmsg.Write((byte)DataTypes.EMPTY);
                client.SendMessage(outmsg, NetDeliveryMethod.ReliableOrdered);
            }
        }

        public static void SetHandler(PacketTypes name, Action<NetIncomingMessage> callback)
        {
            if (!handlers.ContainsKey(name))
            {
                handlers.Add(name, callback);
            }
            else
            {
                handlers[name] = callback;
            }
        }

        private static void IncomingHandler()
        {
            NetIncomingMessage incoming;
            while (IsRunning)
            {
                while ((incoming = client.ReadMessage()) != null)
                {
                    switch (incoming.MessageType)
                    {
                        case NetIncomingMessageType.Data:
                            PacketTypes val = (PacketTypes)incoming.ReadInt16();
                            Logger.WriteLine("Got new message: " + val.ToString());
                            if (val == PacketTypes.GETROOM)
                            {
                                UpdateRoom(incoming);
                            }
                            if (val == PacketTypes.MINIGAME)
                            {
                                MinigameTypes minigame = (MinigameTypes)incoming.ReadInt16();
                                switch (minigame)
                                {
                                    case MinigameTypes.FlySwat: Room.GotoRoom(typeof(Game1.Minigames.FlySwat.FlySwat)); break;
                                    case MinigameTypes.MainGame: Room.GotoRoom(typeof(Game1.Rooms.Room1)); break;
                                    case MinigameTypes.FollowTheLeader: Room.GotoRoom(typeof(Game1.Minigames.FollowTheLeader.FollowTheLeader)); break;
                                    case MinigameTypes.ClimbTheMountain: Room.GotoRoom(typeof(Game1.Minigames.ClimbTheMountain.ClimbTheMountain)); break;
                                    case MinigameTypes.DinoCollectStuff: Room.GotoRoom(typeof(Game1.Minigames.DinoCollectStuff.DinoCollectStuff)); break;
                                    default: Room.GotoRoom(typeof(Game1.Rooms.DebugRoom)); break;
                                }
                            }
                            if (handlers.ContainsKey(val))
                            {
                                handlers[val].Invoke(incoming);
                            }
                            break;

                        default:
                            break;
                    }
                }
                Thread.Sleep(10);
            }
            client.Disconnect("DOEI");
        }

        private static void UpdateRoom(NetIncomingMessage inc)
        {
            lock (StateLock)
            {
                GameStateList.Clear();
                int playerCount = inc.ReadInt32();
                for (int i = 0; i < playerCount; i++)
                {
                    GameStateList.Add(new Player(inc.ReadInt32(),
                                                 inc.ReadString(),
                                                 inc.ReadFloat(),
                                                 inc.ReadFloat(),
                                                 inc.ReadFloat(),
                                                 inc.ReadFloat(), inc.ReadInt32()));
                }
            }
            lock (DataLock)
            {
                if (GameData != null)
                {
                    GameData.Close();
                    GameData.Dispose();
                }
                int length = inc.ReadInt32();
                if (length == 0)
                {
                    GameData = null;
                }
                else
                {
                    GameData = new MemoryStream(length);
                    for (int i = 0; i < length; i++)
                    {
                        GameData.WriteByte(inc.ReadByte());
                    }
                }
            }
        }

        public static void Stop()
        {
            IsRunning = false;
            client.Disconnect("Cya");
        }
    }
    enum PacketTypes
    {
        LOGINSESSID = 0, JOINROOM = 1, LEAVEROOM = 2, GETROOM = 3, ROOMLIST = 4, CREATEROOM = 5,
        MINIGAME = 6, SETMOVE = 7, DOMOVE = 8, MOUSE = 9, PLAYER = 10, TICK = 11, SWAT = 12, CHAT = 13
    }

    enum DataTypes
    {
        BYTE = 0, FLOAT = 1, STRING = 2, INT = 3, EMPTY = 4
    }

    enum MinigameTypes
    {
        None = 0, MainGame = 1, FlySwat = 2, ClimbTheMountain = 3, DinoCollectStuff = 4, FollowTheLeader = 5
    }
}