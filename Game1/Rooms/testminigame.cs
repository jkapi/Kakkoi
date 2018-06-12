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
using Game1.Helpers;
using System.Diagnostics;

namespace Game1.Rooms
{
    class Testminigame : Room
    {
        SpriteFont Arial;
        Button btn;
        public Task<HttpResponseMessage> haalvraagop;
        private Sprite covers;
        private Model kip;
        D3D d3d;

        public override void Initialize()
        {
            d3d = new D3D(Graphics, new RenderTarget2D(GraphicsDevice, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight, false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8, 0, RenderTargetUsage.DiscardContents));
            Arial = Content.Load<SpriteFont>("arial16");
            Objects.Add(new GameObjects.GameList(Vector2.Zero));
            btn = new Button(new Vector2(20, 20), new Vector2(200, 30), Arial, "Ga naar room1");
            btn.OnClick += gomain;
            Objects.Add(btn);
            Objects.Add(new TextBoxAdvanced(new Vector2(20, 80), new Vector2(300, 200), Arial, "Vul iets in", false, "", true));
            Objects.Add(new CheckBox(new Vector2(400, 30), true) { Text = "Jeej" });
            Objects.Add(new CheckBox(new Vector2(400, 60), false));
            haalvraagop = new HttpClient().GetAsync("http://kakoi.ml/quiz.php");
            covers = new Sprite(Content.Load<Texture2D>("roomselect/gamecovers"), 4);
            kip = Content.Load<Model>("random/chickenV2");
        }

        private void gomain(object sender, EventArgs e)
        {
            GotoRoom(typeof(Room1));
        }

        public override void PreDraw()
        {
            DrawGround();
        }

        public override void Draw()
        {
            View.DrawText(Arial, "Boze tekst", new Vector2(20, 40));
            string screenResolutions = "";
            foreach (var mode in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {
                if (mode.Width >= 800 && mode.Height >= 768)
                    screenResolutions += mode.Width + "x" + mode.Height + "\n";
            }
            if (haalvraagop.Status == TaskStatus.RanToCompletion)
            {
                var s = haalvraagop.Result.Content.ReadAsStringAsync();
                s.Wait();
                dynamic vraag = JObject.Parse(s.Result);
                View.DrawText(Arial, vraag, new Vector2(800, 800));
            }
            View.DrawText(Arial, screenResolutions, new Vector2(600, 10));
        }

        public override void PostDraw()
        {
            View.DrawRenderTarget(d3d.RenderTarget, new Vector2(0, 0));
        }

        void DrawGround()
        {
            Stopwatch s = Stopwatch.StartNew();
            d3d.Begin();
            Matrix translate = Matrix.CreateTranslation(500f, 200f, 0f) + Matrix.CreateFromYawPitchRoll((float)-Math.Cos(GameTime.TotalGameTime.TotalSeconds), (float)Math.Sin(GameTime.TotalGameTime.TotalSeconds / 2), (float)-Math.Cos(GameTime.TotalGameTime.TotalSeconds / 3));
            d3d.DrawSprite(covers, 1, translate);
            //translate = Matrix.CreateTranslation(0, 0, 10f) * Matrix.CreateScale(0.05f) * Matrix.CreateRotationY((float)(GameTime.TotalGameTime.TotalSeconds) / 2f);
            translate = Matrix.CreateScale(0.05f) * Matrix.CreateFromYawPitchRoll((float)GameTime.TotalGameTime.TotalSeconds, (float)Math.Sin(GameTime.TotalGameTime.TotalSeconds / 4) / 2, 0) * Matrix.CreateTranslation(new Vector3(0,0,0.5f));
            d3d.DrawModel(kip, translate);
            d3d.End();
            s.Stop();
        }
    }
}
