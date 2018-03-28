using Game1.StrangerCade.Framework.Multiplayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;
using StrangerCade.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Rooms
{
    class Testminigame : Room
    {
        SpriteFont Arial;
        Button btn;
        public override void Initialize()
        {
            Arial = Content.Load<SpriteFont>("Arial");
            btn = new Button(new Vector2(20, 20), new Vector2(200, 30), Arial, "Ga naar room1");
            btn.OnClick += gomain;
            Objects.Add(btn);
        }

        private void gomain(object sender, EventArgs e)
        {
            GotoRoom(typeof(Room1));
        }

        public override void Draw()
        {
            View.DrawText(Arial, "Piemels", new Vector2(20, 40));
        }
    }
}
