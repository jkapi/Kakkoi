using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StrangerCade.Framework;


namespace Game1.Minigames.DinoCollectStuff
{
    class Drugs : GameObject
    {
        Sprite Drugssprite;
        Vector2 leftSide;
        Vector2 rightSide;
        Random random;
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
        }

        public override void Draw()
        {
            base.Draw();
            View.DrawSpriteStretched(Drugssprite, 0, Position, new Vector2(50,50));
        }

        //public override void Update()
        //{
        //    base.Update();  
        //}
    }
  
}
