using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrangerCade.Framework.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    class Player : GameObject
    {
        private Sprite Eyes;
        private Sprite Mouths;

        private int EyesIndex = 0;
        private int MouthsIndex = 0;

        public Player(Vector2 position) : base(position)
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            if (Keyboard.CheckPressed(Keys.Up))
            {
                EyesIndex++;
                if (EyesIndex == Eyes.SubImages.Count)
                {
                    EyesIndex = 0;
                }
            }
            if (Keyboard.CheckPressed(Keys.Down))
            {
                EyesIndex--;
                if (EyesIndex == -1)
                {
                    EyesIndex = Eyes.SubImages.Count - 1;
                }
            }
            if (Keyboard.CheckPressed(Keys.Right))
            {
                MouthsIndex++;
                if (MouthsIndex == Mouths.SubImages.Count)
                {
                    MouthsIndex = 0;
                }
            }
            if (Keyboard.CheckPressed(Keys.Left))
            {
                MouthsIndex--;
                if (MouthsIndex == -1)
                {
                    MouthsIndex = Mouths.SubImages.Count - 1;
                }
            }
        }

        public override void Draw()
        {
            View.DrawSprite(Eyes, EyesIndex, Position);
            View.DrawSprite(Mouths, MouthsIndex, Position);
        }
    }
}
