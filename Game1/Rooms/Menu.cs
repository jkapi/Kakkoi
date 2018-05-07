using StrangerCade.Framework;
using Game1.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game1.Rooms
{
    class Menu : Room
    {
        private Texture2D KakoiLogo;

        public override void Initialize()
        {
            MovingBackground.Init(Content);
            KakoiLogo = Content.Load<Texture2D>("roomselect/menukakoilogo");
        }

        public override void Update()
        {

        }

        public override void Draw()
        {
            MovingBackground.Draw(this);
            View.DrawTexture(KakoiLogo, new Vector2(800, 540), null, 0, KakoiLogo.Bounds.Center.ToVector2());
        }
    }
}
