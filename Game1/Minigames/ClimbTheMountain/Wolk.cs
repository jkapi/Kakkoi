using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;

namespace Game1.Minigames.ClimbTheMountain
{
    class Wolk : GameObject
    {
        public Texture2D picture { get; set; }
        public Vector2 widthLength { get; private set; }
        public int speed { get; private set; }
        public Vector2 resolution { get; private set; }
        public Timer timer { get; private set; }

        public string switchSide { get; private set; }
        static public Random random { get; private set; } = new Random();

        public Wolk(Texture2D Picture, Vector2 Position, Vector2 WidthLength, Vector2 Resolution) : base(Position)
        {
            picture = Picture;
            widthLength = WidthLength;
            resolution = Resolution;
            speed = random.Next(5,20);
            
        }

        public override void Update()
        {
            Vector2 oldPos = new Vector2(0, 0);
            oldPos = Position;
            Position.X += speed * (float)GameTime.ElapsedGameTime.TotalSeconds;
            if (Position.X > resolution.X || Position.X < - picture.Width)
            {
                speed = -speed;
            }
        }

        public override void Draw()
        {
            View.DrawTexture(picture, Position);
        }

    }
}
