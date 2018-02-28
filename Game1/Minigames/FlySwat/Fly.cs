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
        private Rectangle bounds;
        bool flying = true;
        public Fly(Vector2 position, Vector2 startDirection, int randomSeed, Rectangle bounds) : base(position)
        {
            random = new Random(randomSeed);
            this.bounds = bounds;
        }

        public override void Initialize()
        {
            Sprite = new Sprite(Room.Content.Load<Texture2D>("minigame/flyswat/fly"), 2);
            SpriteSpeed = 10;
        }

        public override void Update()
        {
            if (flying)
            {
                if (waitframes == 0)
                {
                    flying = (random.Next(0,6) > 1);
                    speed = new Vector2(random.Next(0, 1) * 8, random.Next(0, 1) * 8);
                }
            }
        }
    }
}