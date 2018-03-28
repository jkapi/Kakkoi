using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Lidgren.Network;

namespace Game1.StrangerCade.Framework.Multiplayer
{
    class SocketIOHandler
    {
        static bool IsRunning = true;
        static NetClient client;
        static List<Player> GameStateList;
        public static bool Connected { get; private set; } = false;
        static string sessid = "";
        static Dictionary<PacketTypes, Action<NetIncomingMessage>> handlers = new Dictionary<PacketTypes, Action<NetIncomingMessage>>();
        public static void Connect(string sessid, string ip)
        {
           // try
            //{
                NetPeerConfiguration config = new NetPeerConfiguration("Kakoi");
                client = new NetClient(config);
                NetOutgoingMessage outmsg = client.CreateMessage();


                client.Start();

                outmsg.Write((short)PacketTypes.LOGINSESSID);
                outmsg.Write(sessid);

                client.Connect(ip, 25000, outmsg);

                GameStateList = new List<Player>();

                new Thread(IncomingHandler).Start();

                Connected = true;
            //}
            //catch
            //{
            //    Connected = false;
            //}
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

        public static void SendMessage(PacketTypes type, params float[] data)
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

        public static void SendMessage(PacketTypes type, params string[] data)
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

        public static void SendMessage(PacketTypes type, params int[] data)
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
            while (true)
            {
                while ((incoming = client.ReadMessage()) != null)
                {
                    switch (incoming.MessageType)
                    {
                        case NetIncomingMessageType.Data:
                            PacketTypes val = (PacketTypes)incoming.ReadInt16();
                            Logger.WriteLine("Got new message: " + val.ToString());
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
        }
    }

    enum PacketTypes
    {
        LOGINUSERPASS, LOGINSESSID, JOINROOM, LEAVEROOM, GETROOM, ROOMLIST,
        MINIGAME, SETMOVE, DOMOVE, MOUSE, PLAYER
    }

    enum DataTypes
    {
        BYTE, FLOAT, STRING, INT
    }
}
