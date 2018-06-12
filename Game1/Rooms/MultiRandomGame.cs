using Game1.GameObjects;
using Microsoft.Xna.Framework;
using StrangerCade.Framework;
using StrangerCade.Framework.Multiplayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Rooms
{
    class MultiRandomGame : Room
    {
        bool started = false;
        GameList gamelist;
        public override void Update()
        {
            if (!started)
            {
                try
                {
                    var data = SocketHandler.GetData();
                    if (data.BaseStream.Length == 8)
                    {
                        gamelist = new GameList(Vector2.Zero, 5, true, 0, data.ReadSingle(), data.ReadSingle());
                        Objects.Add(gamelist);
                        started = true;
                    }
                    SocketHandler.SendMessage(PacketTypes.MOUSE, Mouse.Position.X, Mouse.Position.Y, Mouse.AverageMovement().X, Mouse.AverageMovement().Y);
                }
                catch
                {

                }
            }
            else
            {
                if (gamelist.speed == 0)
                {
                    SocketHandler.SendMessage(PacketTypes.MOUSE, Mouse.Position.X + 0.5f, Mouse.Position.Y + 0.1f + (gamelist.bottompanel/10f), Mouse.AverageMovement().X, Mouse.AverageMovement().Y);
                }
                else
                {
                    SocketHandler.SendMessage(PacketTypes.MOUSE, Mouse.Position.X, Mouse.Position.Y, Mouse.AverageMovement().X, Mouse.AverageMovement().Y);
                }
            }
        }
        public override void Draw()
        {
            MovingBackground.Draw(this);
        }
    }
}
