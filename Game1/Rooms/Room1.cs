using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrangerCade.Framework.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using StrangerCade.Framework;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using Game1.GameObjects;
using StrangerCade.Framework.Multiplayer;

namespace Game1.Rooms
{
    class Room1 : Room
    {
        SpriteFont Arial12;
        SpriteFont Arial24;
        Sprite Chips;
        Sprite Heads;

        MainBoard Board;
        int[] Rotations = new int[4] { 0, 0, 0, 0 };

        Vector2 _vector64 = new Vector2(64);
        
        float overlayOpacity = 0;
        
        float MouseAngle = 0;

        public override void Initialize()
        {
            Mouse.Cursor = MouseCursor.FromTexture2D(Content.Load<Texture2D>("transparant"),0,0);
            View.RotationMode = View.RotationType.Degrees;

            Arial12 = Content.Load<SpriteFont>("OpenSans13");
            Arial24 = Content.Load<SpriteFont>("arial24");
            Chips = new Sprite(Content.Load<Texture2D>("chips"));
            Heads = new Sprite(Content.Load<Texture2D>("random/heads"), 4);

            Board = new MainBoard(new Vector2(600, 32));

            Objects.Add(Board);
            Objects.Add(new Chatbox());
        }

        public override void Update()
        {
            Rotations[Board.CurrentPlayer]++;
            if (Rotations[Board.CurrentPlayer] > 360) { Rotations[Board.CurrentPlayer] -= 360; }
            if (overlayOpacity > 1) { overlayOpacity = 1; }
        }

        public override void Draw()
        {
            var players = SocketHandler.GetPlayers();

            MovingBackground.Draw(this);
            View.DrawRectangle(new Rectangle(600, 32, 640, 640), false, new Color(240, 240, 240));

            View.DrawSprite(Chips, 0, new Vector2(32, 32) + _vector64, null, Rotations[0], _vector64);
            View.DrawSprite(Chips, 1, new Vector2(32, 192) + _vector64, null, Rotations[1], _vector64);
            View.DrawSprite(Chips, 2, new Vector2(32, 352) + _vector64, null, Rotations[2], _vector64);
            View.DrawSprite(Chips, 3, new Vector2(32, 512) + _vector64, null, Rotations[3], _vector64);

            View.DrawSpriteStretched(Heads, 3, new Vector2(76, 76), new Vector2(40));
            View.DrawSpriteStretched(Heads, 0, new Vector2(76, 76 + 160), new Vector2(40));
            View.DrawSpriteStretched(Heads, 1, new Vector2(76, 76 + 320), new Vector2(40));
            View.DrawSpriteStretched(Heads, 2, new Vector2(76, 76 + 480), new Vector2(40));

            try
            {
                DrawTextOutlined(Arial24, players[0].Name, new Vector2(180, 96), Color.White, Color.Black);
                if (players.Count > 1)
                    DrawTextOutlined(Arial24, players[1].Name, new Vector2(180, 256), Color.White, Color.Black);
                if (players.Count > 2)
                    DrawTextOutlined(Arial24, players[2].Name, new Vector2(180, 416), Color.White, Color.Black);
                if (players.Count > 3)
                    DrawTextOutlined(Arial24, players[3].Name, new Vector2(180, 576), Color.White, Color.Black);
            }
            catch
            {
                // This isn't really something to worry about, can be caused because of a breakpoint in the tick function of the server
            }
            Vector2 MouseMovement = Mouse.AverageMovement();
            if (Math.Abs(Mouse.LastMovement.X + Mouse.LastMovement.Y) > 0.1)
            {
                if (Math.Abs(MouseMovement.X + MouseMovement.Y) > 0)
                {
                    MouseAngle = (float)Math.Atan2(MouseMovement.Y, MouseMovement.X);
                }
            }
            View.RotationMode = View.RotationType.Radians;
            View.DrawRectangle(Mouse.Position, new Vector2(20), rotation: MouseAngle + (float)Math.PI);
            View.RotationMode = View.RotationType.Degrees;
        }

        public override void PostDraw()
        {
            View.DrawRectangle(new Rectangle(0, 0, 1920, 1080), false, new Color(overlayOpacity, overlayOpacity, overlayOpacity, overlayOpacity));
        }

        private void DrawTextOutlined(SpriteFont font, object text, Vector2 position, Color textColor, Color outlineColor, int borderWidth = 1)
        {
            // Measure full height of text
            Vector2 stringSize = font.MeasureString("Aj");

            for (int x = -borderWidth; x <= borderWidth; x++)
            {
                for (int y = -borderWidth; y <= borderWidth; y++)
                {
                    View.DrawText(font, text, position + new Vector2(x, y), outlineColor, 0, new Vector2(0, stringSize.Y / 2));
                }
            }
            View.DrawText(font, text, position, textColor, 0, new Vector2(0, stringSize.Y / 2));
        }
    }
}
