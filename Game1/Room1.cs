using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrangerCade.Framework.GameObjects.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using StrangerCade.Framework.GameObjects;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    class Room1 : Room
    {
        SpriteFont Arial;
        public override void Initialize()
        {
            //Graphics.IsFullScreen = false;
            //Graphics.PreferredBackBufferHeight = 540;
            //Graphics.PreferredBackBufferWidth = 960;
            //View.Scale = new Vector2(0.5f);
            //Graphics.ApplyChanges();
            Mouse.Cursor = MouseCursor.FromTexture2D(Content.Load<Texture2D>("transparant"),0,0);
            View.RotationMode = View.RotationType.Degrees;
            Arial = Content.Load<SpriteFont>("arial");
            Objects.Add(new MainBoard(Vector2.Zero));
        }

        public override void Update()
        {

        }

        public override void Draw()
        {
        }
    }
}
