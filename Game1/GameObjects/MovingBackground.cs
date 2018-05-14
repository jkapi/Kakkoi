using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.GameObjects
{
    static class MovingBackground
    {
        private static bool initialized = false;

        private static Texture2D BackBlur;
        private static Texture2D BackTriangles;

        private static Vector2 BackTrianglesPosition;
        public static Vector2 BackTrianglesSpeed;

        public static void Init(ContentManager Content)
        {
            if (!initialized)
            {
                initialized = true;
                BackBlur = Content.Load<Texture2D>("RoomSelect/back");
                BackTriangles = Content.Load<Texture2D>("RoomSelect/triangle");
                Random rand = new Random();
                BackTrianglesPosition = new Vector2(rand.Next(0, 1280), rand.Next(0, 720));
                BackTrianglesSpeed = new Vector2(((float)rand.Next(-200, 200)) / 200, ((float)rand.Next(-200, 200)) / 200);
            }
        }

        public static void Draw(this Room room)
        {
            if (!initialized) { Init(room.Content); }

            BackTrianglesPosition += BackTrianglesSpeed;
            if (BackTrianglesPosition.X > 1280) { BackTrianglesPosition.X -= 1280; }
            if (BackTrianglesPosition.X < 0) { BackTrianglesPosition.X += 1280; }
            if (BackTrianglesPosition.Y > 720) { BackTrianglesPosition.Y -= 720; }
            if (BackTrianglesPosition.Y < 0) { BackTrianglesPosition.Y += 720; }

            room.View.DrawTexture(BackBlur, new Vector2(Math.Min(-(room.Mouse.Position.X / 20),0), Math.Min(-room.Mouse.Position.Y / 20,0)));

            Vector2 mouseOffset = new Vector2(-(room.Mouse.Position.X / 1000), -room.Mouse.Position.Y / 100);

            room.View.DrawTexture(BackTriangles, BackTrianglesPosition + mouseOffset);
            room.View.DrawTexture(BackTriangles, BackTrianglesPosition - BackTriangles.Bounds.Size.ToVector2() + mouseOffset);
            room.View.DrawTexture(BackTriangles, BackTrianglesPosition - new Vector2(BackTriangles.Bounds.Size.X, 0) + mouseOffset);
            room.View.DrawTexture(BackTriangles, BackTrianglesPosition - new Vector2(0, BackTriangles.Bounds.Size.Y) + mouseOffset);
        }
    }
}
