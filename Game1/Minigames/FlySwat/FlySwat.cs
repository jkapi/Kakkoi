using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrangerCade.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework.Multiplayer;
using System.Diagnostics;
using Lidgren.Network;

namespace Game1.Minigames.FlySwat
{
    class FlySwat : Room
    {
        int sendNewLocationDelay = 2;
        int tick = 0;
        SpriteFont Arial;
        Sprite meppers;
        Sprite fly;
        Dictionary<int, Vector2> positions;
        public override void Initialize()
        {
            Arial = Content.Load<SpriteFont>("arial16");
            meppers = new Sprite(Content.Load<Texture2D>("minigame/flyswat/vliegennmepper"), 4);
            fly = new Sprite(Content.Load<Texture2D>("minigame/flyswat/fly"), 2);
            Mouse.SetCursor(Content.Load<Texture2D>("transparant"),Point.Zero);
            positions = new Dictionary<int, Vector2>();
        }

        public override void Update()
        {
            if (tick % sendNewLocationDelay == 0)
            {
                SocketHandler.SendMessage(PacketTypes.MOUSE, Mouse.Position.X, Mouse.Position.Y, Mouse.AverageMovement().X, Mouse.AverageMovement().Y);
            }
            var data = SocketHandler.GetData();
            if (data != null)
            {
                if (data.BaseStream.Length > 1)
                {
                    data.BaseStream.Position = 0;
                    positions.Clear();
                    int numflies = data.ReadInt32();
                    for (int i = 0; i < numflies; i++)
                    {
                        try
                        {
                            positions.Add(data.ReadInt32(), new Vector2(data.ReadSingle(), data.ReadSingle()));
                        }
                        catch
                        { }
                    }
                }
            }
            
            if (Mouse.CheckPressed(MouseButtons.Left))
            {
                Rectangle bounds = new Rectangle(Mouse.Position.ToPoint() - new Point(10, 8), new Point(30, 38));
                foreach(var fly in positions)
                {
                    if (bounds.Contains(fly.Value))
                    {
                        SocketHandler.SendMessage(PacketTypes.SWAT, fly.Key);
                    }
                }
            }

            tick++;
        }

        public override void Draw()
        {
            foreach (var position in positions)
            {
                View.DrawSprite(fly, 0, position.Value);
            }

            List<Player> players = SocketHandler.GetPlayers();
            int mynum = 0;
            foreach (Player player in players)
            {
                if (player.Name != SocketHandler.PlayerName)
                {
                    View.DrawSprite(meppers, player.NumInRoom % 4, new Vector2(player.MouseX, player.MouseY));
                    View.DrawText(Arial, player.Name, new Vector2(player.MouseX, player.MouseY));
                }
                else
                {
                    mynum = player.NumInRoom % 4;
                }
            }
            View.DrawSprite(meppers, mynum, Mouse.Position);
            View.DrawText(Arial, GameTime.ElapsedGameTime.TotalMilliseconds, Vector2.Zero);
        }
    }
}
