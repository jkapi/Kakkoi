using StrangerCade.Framework.Multiplayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;
using StrangerCade.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Net.Http;

namespace Game1.Rooms
{
    class Testminigame : Room
    {
        SpriteFont Arial;
        Button btn;
        public Task<HttpResponseMessage> haalvraagop;
        public override void Initialize()
        {
            Arial = Content.Load<SpriteFont>("arial16");
            //btn = new Button(new Vector2(20, 20), new Vector2(200, 30), Arial, "Ga naar room1");
            //btn.OnClick += gomain;
            //Objects.Add(btn);
            //Objects.Add(new TextBoxAdvanced(new Vector2(20, 80), new Vector2(300, 200), Arial, "Vul iets in", false, "", true));
            //Objects.Add(new CheckBox(new Vector2(400, 30), true) { Text = "Jeej" });
            //Objects.Add(new CheckBox(new Vector2(400, 60), false));
            //haalvraagop = new HttpClient().GetAsync("http://kakoi.ml/quiz.php");
            Objects.Add(new GameObjects.GameList(Vector2.Zero));
        }

        private void gomain(object sender, EventArgs e)
        {
            GotoRoom(typeof(Room1));
        }

        public override void Draw()
        {
        //    View.DrawText(Arial, "Boze tekst", new Vector2(20, 40));
        //    string screenResolutions = "";
        //    foreach(var mode in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
        //    {
        //        if (mode.Width >= 800 && mode.Height >= 768)
        //            screenResolutions += mode.Width + "x" + mode.Height + "\n";
        //    }
        //    if (haalvraagop.Status == TaskStatus.RanToCompletion)
        //    {
        //        var s = haalvraagop.Result.Content.ReadAsStringAsync();
        //        s.Wait();
        //        dynamic vraag = JObject.Parse(s.Result);
        //        View.DrawText(Arial, vraag, new Vector2(800, 800));
        //    }
        //    View.DrawText(Arial, screenResolutions, new Vector2(600, 10));
        }
    }
}
