
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using StrangerCade.Framework.UI;

namespace Game1.Minigames.Quiz
{
    class Quiz : Room
    {

        int ronde = 1;

        //DialogResult messageFout;
        Timer msgTimer;
        int Pressed = 0;
        int Score;
        int _Levens = 3;
        string[] _vragen;
        string Vraag1;
        int Min = 1;
        int Max = 3;
        int _Antwoord = 3;

        Texture2D background;
        SpriteFont fontArial;
        Rectangle boxVraag;
        Rectangle BoxAntwoord;
        Rectangle Punten;
        Rectangle Levens;
        Rectangle Timer;
        Rectangle _Progress;
        Rectangle Goed_Fout;

        //
        Timer afteller;
        int countdown;

        //
        SpriteFont Arial;

        Button btnVerder;
        Button btnA;
        Button btnB;
        Button btnC;
        bool disabledBtnA;
        bool disabledBtnB;
        bool disabledBtnC;

        Vraag test = Vraag.GetVraag();
        public override void Initialize()
        {
            base.Initialize();

            afteller = new System.Timers.Timer();
            afteller.Interval = 500;
            afteller.Elapsed += new ElapsedEventHandler(aftellerFunctie);
            afteller.Enabled = true;

            countdown = 30;

            boxVraag = new Rectangle(0, 0, 750, 100);
            boxVraag.Location = new Point(Graphics.PreferredBackBufferWidth / 2 - boxVraag.Width / 2, Graphics.PreferredBackBufferHeight / 4 - boxVraag.Height / 2);

            BoxAntwoord = new Rectangle(0, 0, 400, 175);
            BoxAntwoord.Location = new Point(Graphics.PreferredBackBufferWidth / 2 - BoxAntwoord.Width / 2, Graphics.PreferredBackBufferHeight / 2 - BoxAntwoord.Height / 2);

            Punten = new Rectangle(0, 0, 200, 40);
            Punten.Location = new Point(boxVraag.Location.X + 760, boxVraag.Y);

            Levens = new Rectangle(0, 0, 200, 40);
            Levens.Location = new Point(boxVraag.Location.X + 760, boxVraag.Y + 60);

            Timer = new Rectangle(0, 0, 200, 40);
            Timer.Location = new Point(boxVraag.Location.X - 210, boxVraag.Y);

            _Progress = new Rectangle(0, 0, 225, 25);
            _Progress.Location = new Point(Graphics.PreferredBackBufferWidth / 2 - _Progress.Width / 2, boxVraag.Y - 25);

            Goed_Fout = new Rectangle(0, 0, 40,175);
            Goed_Fout.Location = new Point(BoxAntwoord.X -40, BoxAntwoord.Y);



            Arial = Content.Load<SpriteFont>("arial16");
            btnVerder = new Button(new Vector2(Graphics.PreferredBackBufferWidth / 2 - 75, (boxVraag.Y + boxVraag.Height) + ((BoxAntwoord.Y - boxVraag.Y) / 4)), new Vector2(150, 30), Arial, "Verder");
            btnVerder.OnClick += verderClick;
            //Objects.Add(btnVerder);

            btnA = new Button(new Vector2(BoxAntwoord.Location.X + 20, BoxAntwoord.Location.Y + 25), new Vector2(25, 25), Arial, "A");
            btnA.OnClick += aClick;
            btnA.ColorBackground = Color.Orange;
            btnA.ColorBackgroundUnfocussed = Color.Orange;
            btnA.ColorBackgroundHover = Color.OrangeRed;
            Objects.Add(btnA);
            bool disabledBtnA = false;

            btnB = new Button(new Vector2(BoxAntwoord.Location.X + 20, BoxAntwoord.Location.Y + 75), new Vector2(25, 25), Arial, "B");
            btnB.OnClick += bClick;
            btnB.ColorBackground = Color.Orange;
            btnB.ColorBackgroundUnfocussed = Color.Orange;
            btnB.ColorBackgroundHover = Color.OrangeRed;

            Objects.Add(btnB);
            bool disabledBtnB = false;

            btnC = new Button(new Vector2(BoxAntwoord.Location.X + 20, BoxAntwoord.Location.Y + 125), new Vector2(25, 25), Arial, "C");
            btnC.OnClick += cClick;
            btnC.ColorBackground = Color.Orange;
            btnC.ColorBackgroundUnfocussed = Color.Orange;
            btnC.ColorBackgroundHover = Color.OrangeRed;
            Objects.Add(btnC);
            bool disabledBtnC = false;




            //Graphics.PreferredBackBufferWidth/2
            //Graphics.PreferredBackBufferWidth/4

            //Graphics.PreferredBackBufferWidth / 2 - Graphics.PreferredBackBufferWidth/4
            //Graphics.PreferredBackBufferWidth / 2 - Graphics.PreferredBackBufferWidth/4/2


            background = Content.Load<Texture2D>("minigame/Quiz/woonwijkAchtergrond");

            fontArial = Content.Load<SpriteFont>("arial16");

        }
        public override void Update()
        {
            base.Update();
            Mouse.Cursor = Mouse.DefaultCursor;
        }
        public override void Draw()
        {
            base.Draw();
            View.DrawTexture(background, new Vector2(0, 0));
            View.DrawTextureStretched(background, new Vector2(0, 0), new Vector2(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight));

            View.DrawRectangle(boxVraag, true, Color.Black);
            View.DrawRectangle(boxVraag, false, Color.White);

            //View.DrawText(fontArial, "Oefenvraag : Wat is 1+1?", new Vector2(boxVraag.Location.X + 4, boxVraag.Location.Y + 4));
            View.DrawText(fontArial, "Wat is 1 + 1 ", new Vector2(boxVraag.Location.X + 4, boxVraag.Location.Y + 4));

            // Informatie

            View.DrawRectangle(Punten, false, Color.White);
            View.DrawRectangle(Punten, true, Color.Black);
            View.DrawText(fontArial, "Score: " + " " + Score, new Vector2(Punten.Location.X + 4, Punten.Location.Y + 4));

            View.DrawRectangle(Goed_Fout, false, Color.White);
            View.DrawRectangle(Goed_Fout, true, Color.Black);

            View.DrawRectangle(Levens, false, Color.White);
            View.DrawRectangle(Levens, true, Color.Black);
            View.DrawText(fontArial, "Levens: " + _Levens, new Vector2(Levens.Location.X + 4, Levens.Location.Y + 4));

            View.DrawRectangle(Timer, false, Color.White);
            View.DrawRectangle(Timer, true, Color.Black);



            View.DrawRectangle(_Progress, false, Color.Gray);
            View.DrawRectangle(_Progress, true, Color.Black);



            //Antwoord
            View.DrawRectangle(BoxAntwoord, false, Color.White);
            View.DrawRectangle(BoxAntwoord, true, Color.Black);


            View.DrawText(fontArial, "69", new Vector2(btnA.Position.X + 35, btnA.Position.Y));
            View.DrawText(fontArial, "21", new Vector2(btnB.Position.X + 35, btnB.Position.Y));
            View.DrawText(fontArial, "2", new Vector2(btnC.Position.X + 35, btnC.Position.Y));


            if (countdown > 10)
            {
                View.DrawText(fontArial, "00:" + countdown, new Vector2(Timer.Location.X + 4, Timer.Location.Y + 4));
            }
            else if (countdown < 10 & countdown > -1)
            {
                View.DrawText(fontArial, "00:0" + countdown, new Vector2(Timer.Location.X + 4, Timer.Location.Y + 4));
            }

        }

        private void aftellerFunctie(object source, ElapsedEventArgs e)
        {
            if (countdown < 31 & countdown > 0)
            {
                countdown -= 1;
            }
        }

        private void verderClick(object sender, EventArgs e)
        {
            Pressed += 1;
            Objects.Remove(btnVerder);
            disabledBtnA = false;
            disabledBtnB = false;
            disabledBtnC = false;
            countdown = 30;
            afteller.Start();

        }

        private void aClick(object sender, EventArgs e)
        {
            afteller.Stop();
            if (disabledBtnA == false)
            {
                Objects.Add(btnVerder);
                if (_Antwoord == 1)
                {
                    if (Pressed == 0)
                    {

                    }
                    else
                    {
                        Score = Score + 1000 + (50 * countdown);

                    }


                }
                else
                {
                    if (Pressed == 0)
                    {

                    }
                    else
                    {
                        if (_Levens > 0)
                        {
                            _Levens -= 1;
                        }
                       
                    }
                }
                disabledBtnA = true;
            }

        }

        private void bClick(object sender, EventArgs e)
        {
            afteller.Stop();
            if (disabledBtnB == false)
            {
                Objects.Add(btnVerder);
                if (_Antwoord == 2)
                {
                    if (Pressed == 0)
                    {

                    }
                    else
                    {
                        Score = Score + 1000 + (50 * countdown);

                    }


                }
                else
                {
                    if (_Levens > 0)
                    {
                        _Levens -= 1;
                    }
                }
                disabledBtnB = true;
            }
        }

        private void cClick(object sender, EventArgs e)
        {
            afteller.Stop();
            if (disabledBtnC == false)
            {
                Objects.Add(btnVerder);
                if (_Antwoord == 3)
                {
                    if (Pressed == 0)
                    {

                    }
                    else
                    {
                        Score = Score + 1000 + (50 * countdown);

                    }


                }
                else
                {
                    if (_Levens > 0)
                    {
                        _Levens -= 1;
                    }
                }
                disabledBtnC = true;
            }
        }
    }
}
