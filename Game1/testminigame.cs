using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Testminigame : Room
    {
        SpriteFont Arial;
        public override void Initialize()
        {
            Arial = Content.Load<SpriteFont>("Arial");
        }

        public override void Draw()
        {
            View.DrawText(Arial, "Piemels", new Vector2(20, 40));
        }
    }
}
