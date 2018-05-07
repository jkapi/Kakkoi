using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game1.Minigames.QuizAfvalRace
{
    class Test_object : GameObject
    {
        public Test_object(Vector2 pos) : base(pos)
        {

        }
        public override void Update()
        {
            if (Keyboard.Check(Keys.Up))
            {
                Position.Y -= 5; 
            }
            if (Keyboard.Check(Keys.Down))
            {
                Position.Y += 5;
            }
            if (Keyboard.Check(Keys.Left))
            {
                Position.X -= 5;
            }
            if (Keyboard.Check(Keys.Right))
            {
                Position.X += 5;
            }

            if (Mouse.Check(MouseButtons.Left))
            {
                Position.X += 9;

            }

            if (Mouse.Check(MouseButtons.Right))
            {
                Position.X -= 9;

            }
            base.Update();
        }
        public override void Initialize()
        {
            Sprite = new Sprite(Room.Content.Load<Texture2D>("chips"));
            SpriteSpeed = 3;
            SpriteIndex = 6;
            base.Initialize();
        }
    
    }
}
