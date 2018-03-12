using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using StrangerCade.Framework.UI;

namespace Game1.Minigames.QuizAfvalRace
{
    class Quiz_Room : Room
    {
        private SpriteFont arial;

        private Sprite sprite;
        

        
        public override void Initialize()
        {
            sprite = new Sprite(Content.Load<Texture2D>("Muur"));
            Objects.Add(new Test_object(new Vector2(2000,2000)));

            arial = Content.Load<SpriteFont>("Jokerman");
            //var button = new Button(new Vector2(400,200), new Vector2(2000,4), arial, "Klik");
            //Objects.Add(button);
           // button.OnClick += Button_OnClick;
        }

        private void Button_OnClick(object sender, EventArgs e)
        {
            ((Button)sender).Text = "dankje";
        }

        public override void Update()
        {
            base.Update();
        }
        public override void Draw()
        {
            View.DrawSprite(sprite, 0, new Vector2(0, 0));
            View.DrawSetColor(Color.DeepSkyBlue);
            View.DrawText(arial, "blablabla", new Vector2(10, 200));
        }
    }
}
