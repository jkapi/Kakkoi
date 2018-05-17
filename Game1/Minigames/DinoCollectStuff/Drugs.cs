using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StrangerCade.Framework;


namespace Game1.Minigames.DinoCollectStuff
{
    class Drugs : GameObject
    {
        public Sprite Drugssprite;
        public Vector2 leftSide;
        public Vector2 rightSide;
        public Random random;
        public int timer;
        public Timer elkeSeconde;
        public override void Initialize()
        {
            Drugssprite = new Sprite(Room.Content.Load<Texture2D>("minigame/dinozooi/reptile"));
        }

        public Drugs(Vector2 LeftSide, Vector2 RightSide) : base(Vector2.Zero)
        {


            random = new Random();
            leftSide = LeftSide;
            rightSide = RightSide;
            Position = new Vector2(random.Next((int)leftSide.X, (int)rightSide.X), random.Next((int)leftSide.Y, (int)rightSide.Y));
            timer = 10000;

            Timer elkeSeconde = new Timer(1000);
            elkeSeconde.Start();
            elkeSeconde.Elapsed += tickEvent;
        }

        public override void Draw()
        {
            View.DrawSpriteStretched(Drugssprite, 0, Position, new Vector2(50,50));
        }
        private void tickEvent(Object source, ElapsedEventArgs e)
        {
            if (timer > 0)
            {
                timer = timer - 1000;
            }
        }

        //public override void Update()
        //{
        //    base.Update();  
        //}
    }
  
}
