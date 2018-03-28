using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrangerCade.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1.StrangerCade.Framework.Multiplayer;
using Game1.StrangerCade.Framework;
using System.Diagnostics;
using Lidgren.Network;

namespace Game1.Minigames.FlySwat
{
    class FlySwat : Room
    {
        Random random = new Random();
        int respawndelay = 0;
        int sendNewLocationDelay = 2;
        int tick = 0;
        string positionstring = "";
        private Object dataLock = new Object();
        List<Vector4> positions = new List<Vector4>();
        SpriteFont Arial;
        public override void Initialize()
        {
            for (int i = 0; i < 10; i++)
                SpawnFly();
            respawndelay = random.Next(10, 30);
            Arial = Content.Load<SpriteFont>("arial");
            Mouse.SetCursor(new Sprite(Content.Load<Texture2D>("minigame/flyswat/vliegennmepper"), 4), 1, new Point(14,16));
            SocketIOHandler.SendMessage(PacketTypes.MINIGAME, "flyswat");
            SocketIOHandler.SetHandler(PacketTypes.MOUSE, UpdateGameState);
        }

        public override void Update()
        {
            respawndelay--;
            if (respawndelay == 0)
            {
                respawndelay = random.Next(10, 30);
                SpawnFly();
            }
            if (tick % sendNewLocationDelay == 0)
            {
                SocketIOHandler.SendMessage(PacketTypes.MOUSE, Mouse.Position.X, Mouse.Position.Y, Mouse.AverageMovement().X, Mouse.AverageMovement().Y);
            }
            //lock(dataLock)
            //{
            //    for (int i = 0; i < positions.Count; i++)
            //    {
            //        Vector4 cv = positions[i];
            //        positions[i] = new Vector4(cv.X + cv.Z, cv.Y + cv.W, cv.Z, cv.W);
            //    }
            //}
            tick++;
        }

        public void UpdateGameState(NetIncomingMessage s)
        {
            Logger.WriteLine("Got update");
            s.ReadByte();
            int length = s.ReadInt32() / 4;
            List<Vector4> newlist = new List<Vector4>();
            for (int i = 0; i < length; i++)
            {
                newlist.Add(new Vector4(s.ReadFloat(), s.ReadFloat(), s.ReadFloat(), s.ReadFloat()));
            }
            lock (dataLock)
            {
                positions = new List<Vector4>(newlist);
            }
        }

        public override void Draw()
        {
            lock (dataLock)
            {
                foreach (Vector4 position in positions)
                {
                    Debug.WriteLine(position.ToString());
                    View.DrawRectangle(new Vector2(position.X, position.Y), new Vector2(20, 20));
                }
            }
            View.DrawText(Arial, GameTime.ElapsedGameTime.TotalMilliseconds, Vector2.Zero);
            View.DrawText(Arial, positionstring, new Vector2(0, 36));
        }

        private void SpawnFly()
        {
            Objects.Add(new Fly(new Vector2(random.Next(0, 1920), random.Next(0, 1080)), new Vector2(1, 1), random.Next()));
        }
    }
}
