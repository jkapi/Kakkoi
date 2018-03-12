using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Minigames.FlySwat
{
    class Fly : GameObject
    {
        private Random random;
        private Vector2 speed;
        private int waitframes;
        bool flying = true;
        private Rectangle Bounds { get { return new Rectangle((int)Position.X - 5, (int)Position.Y - 11, Sprite.Width + 10, Sprite.Height + 22); } }

        public Fly(Vector2 position, Vector2 startDirection, int randomSeed) : base(position)
        {
            random = new Random(randomSeed);
        }

        public override void Initialize()
        {
            Sprite = new Sprite(Room.Content.Load<Texture2D>("minigame/flyswat/fly"), 2);
            SpriteSpeed = 5;
        }

        public override void Update()
        {
            if (waitframes == 0)
            {
                flying = (random.Next(0, 6) > 1);
                speed = new Vector2(random.Next(-8, 8), random.Next(-8, 8));
                if (!flying)
                {
                    Position += new Vector2(0, 16);
                }
                waitframes = random.Next(10, 30) * (flying ? 1: 2);
            }
            if (flying)
            {
                Position += speed;
            }
            else
            {
                SpriteIndex = 0;
            }
            waitframes--;
            if (Mouse.CheckPressed(MouseButtons.Left))
            {
                // If clicked on the fly
                if (Bounds.Contains(Mouse.Position))
                {
                    Room.Objects.Remove(this);
                }
            }
        }
    }
}