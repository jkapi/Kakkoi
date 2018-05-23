using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Minigames.ClimbTheMountain
{
    class Wolk
    {
        public Texture2D picture { get; set; }
        public Vector2 position { get; private set; }
        public Vector2 widthLength { get; private set; }
        public int speed { get; private set; }
        public Vector2 resolution { get; private set; }
        public Timer timer { get; private set; }

        public string switchSide { get; private set; }
        static public Random random { get; private set; } = new Random();

        public Wolk(Texture2D Picture, Vector2 Position, Vector2 WidthLength, Vector2 Resolution)
        {
            picture = Picture;
            position = Position;
            widthLength = WidthLength;
            resolution = Resolution;
            switchSide = "NaarLinks";
            speed = random.Next(1,5);
            timer = new Timer(75);
            timer.Elapsed += OnTick;
            timer.Enabled = true;
            
        }

        private void OnTick(Object source, ElapsedEventArgs e)
        {
            Vector2 oldPos = new Vector2(0, 0);
            oldPos = position;
            if (switchSide == "NaarLinks")
            {
                position = new Vector2(oldPos.X + speed, oldPos.Y);
                if (position.X > resolution.X)
                {
                    switchSide = "NaarRechts";
                }
            }
            if (switchSide == "NaarRechts")
            {
                position = new Vector2(oldPos.X - speed, oldPos.Y);
                if (position.X < 0)
                {
                    switchSide = "NaarLinks";
                }
            }
        }

    }
}
