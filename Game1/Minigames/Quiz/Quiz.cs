
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;
using StrangerCade.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Game1.Minigames.Quiz
{
    class Quiz : Room
    {
        Texture2D background;
        SpriteFont fontArial;
        Rectangle boxVraag;
        Rectangle BoxAntwoord;
        Rectangle BoxA;
        Rectangle BoxB;
        Rectangle BoxC;
        Rectangle Punten;
        Rectangle Levens;
        Rectangle Timer;
        Rectangle _Progress;
        Timer timer;
        Button start;
        Button A;
        Button B;
        Button C;
        SpriteFont Arial;
        int timer_int;
        int levens;
        int Score;
        string VraagA = "69";
        string VraagB = "21";
        string VraagC = "2";
        string Vraag = "Hoeveel is 1+1 ?";
        int Antwoord = 3;
        int Pressed = 0;
        public override void Initialize()
        {
            base.Initialize();
            boxVraag = new Rectangle(0,0,750,100);
            boxVraag.Location = new Point(Graphics.PreferredBackBufferWidth/2-boxVraag.Width/2, Graphics.PreferredBackBufferHeight / 4 - boxVraag.Height / 2);

            BoxAntwoord = new Rectangle(0,0,400,175);
            BoxAntwoord.Location = new Point(Graphics.PreferredBackBufferWidth / 2 - BoxAntwoord.Width / 2, Graphics.PreferredBackBufferHeight / 2 - BoxAntwoord.Height / 2);

            Punten = new Rectangle(0, 0, 200, 40);
            Punten.Location = new Point(boxVraag.Location.X + 760,boxVraag.Y);

            Levens = new Rectangle(0, 0, 200, 40);
            Levens.Location = new Point(boxVraag.Location.X + 760, boxVraag.Y + 60);

            Timer = new Rectangle(0, 0, 200, 40);
            Timer.Location = new Point(boxVraag.Location.X - 210, boxVraag.Y);

            timer_int = 30;

            _Progress = new Rectangle(0, 0, 225, 25);
            _Progress.Location = new Point(Graphics.PreferredBackBufferWidth / 2 - _Progress.Width / 2, boxVraag.Y -25);


            BoxA = new Rectangle(0, 0, 25, 25);
            BoxA.Location = new Point(BoxAntwoord.Location.X + 20, BoxAntwoord.Y + 25);

            BoxB = new Rectangle(0, 0, 25, 25);
            BoxB.Location = new Point(BoxA.X, BoxA.Y+50);

            BoxC = new Rectangle(0, 0, 25, 25);
            BoxC.Location = new Point(BoxA.X, BoxB.Y + 50);

            timer = new Timer(1000);
            timer.Elapsed += TimerTick;
            timer.Start();

            Arial = Content.Load<SpriteFont>("arial16");

            start = new Button(new Vector2(Graphics.PreferredBackBufferWidth / 2 - 100 , 375), new Vector2(200, 30), Arial, "Verder");
            start.OnClick += btnclick;
            Objects.Add(start);
            start.Activated = false;

            A = new Button(new Vector2(BoxAntwoord.Location.X + 20, BoxAntwoord.Y + 25), new Vector2(25, 25), Arial, "A");
            A.OnClick += btnclick;
            A.ColorBackgroundUnfocussed = Color.OrangeRed;
            Objects.Add(A);

            B = new Button(new Vector2(BoxAntwoord.Location.X + 20, BoxAntwoord.Y + 75), new Vector2(25, 25), Arial, "B");
            B.OnClick += btnclick;
            B.ColorBackgroundUnfocussed = Color.OrangeRed;
            Objects.Add(B);

            C = new Button(new Vector2(BoxAntwoord.Location.X + 20, BoxAntwoord.Y + 125), new Vector2(25, 25), Arial, "C");
            C.OnClick += btnclick;
            C.ColorBackgroundUnfocussed = Color.OrangeRed;
            Objects.Add(C);


            //Graphics.PreferredBackBufferWidth/2
            //Graphics.PreferredBackBufferWidth/4

            //Graphics.PreferredBackBufferWidth / 2 - Graphics.PreferredBackBufferWidth/4
            //Graphics.PreferredBackBufferWidth / 2 - Graphics.PreferredBackBufferWidth/4/2


            background = Content.Load<Texture2D>("minigame/Quiz/woonwijkAchtergrond");

            fontArial = Content.Load<SpriteFont>("arial16");
        }

        public void TimerTick(Object source, ElapsedEventArgs e)
        {
            if (timer_int > 0)
            {
                timer_int = timer_int - 1;
            }
            

        }

        public void Vragen()
        {
            
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
            
            View.DrawRectangle(boxVraag, true,Color.Black);
            View.DrawRectangle(boxVraag, false, Color.White);
           
            //vraag
            View.DrawText(fontArial,Vraag,new Vector2(boxVraag.Location.X + 4, boxVraag.Location.Y+4));

            // Informatie

            View.DrawRectangle(Punten, false, Color.White);
            View.DrawRectangle(Punten, true, Color.Black);
            View.DrawText(fontArial, "Score: " + Score, new Vector2(Punten.Location.X + 4, Punten.Location.Y + 4));

            View.DrawRectangle(Levens, false, Color.White);
            View.DrawRectangle(Levens, true, Color.Black);
            View.DrawText(fontArial, "Levens: 0", new Vector2(Levens.Location.X + 4, Levens.Location.Y + 4));

            View.DrawRectangle(Timer, false, Color.White);
            View.DrawRectangle(Timer, true, Color.Black);
            if (timer_int > 10)
            {


                View.DrawText(fontArial, "00:" + timer_int, new Vector2(Timer.Location.X + 4, Timer.Location.Y + 4));
            }
            else
            {
                View.DrawText(fontArial, "00:0" + timer_int, new Vector2(Timer.Location.X + 4, Timer.Location.Y + 4));
            }

            View.DrawRectangle(_Progress, false, Color.Gray);
            View.DrawRectangle(_Progress, true, Color.Black);



            //Antwoord
            View.DrawRectangle(BoxAntwoord, false, Color.White);
            View.DrawRectangle(BoxAntwoord, true, Color.Black);

            
            View.DrawRectangle(BoxA, false, Color.Orange);
            View.DrawText(fontArial, VraagA, new Vector2(BoxA.Location.X + 35, BoxA.Location.Y));
            View.DrawText(fontArial, "A", new Vector2(BoxA.Location.X + 5, BoxA.Location.Y));


          

            View.DrawRectangle(BoxB, false, Color.Orange);
            View.DrawText(fontArial, VraagB, new Vector2(BoxB.Location.X + 35, BoxB.Location.Y));
            View.DrawText(fontArial, "B", new Vector2(BoxB.Location.X + 5, BoxB.Location.Y));

            View.DrawRectangle(BoxC, false, Color.Orange);
            View.DrawText(fontArial, VraagC, new Vector2(BoxC.Location.X + 35, BoxC.Location.Y));
            View.DrawText(fontArial, "C", new Vector2(BoxC.Location.X + 5, BoxC.Location.Y));

          



            

        }

        private void btnclick(object sender, EventArgs e)
        {
            if (A.Clicked)
            {
                if(Antwoord == 1)
                {
                    Score = Score + (1000 + timer_int * 100);
                }
                else
                {
                    levens = levens - 1;
                }
                timer.Stop();
                A.Activated = false;
                B.Activated = false;
                C.Activated = false;
                start.Activated = true;
            }
            if (B.Clicked)
            {
                if (Antwoord == 2)
                {
                    Score = Score + (1000 + timer_int * 100);
                }
                else
                {
                    levens = levens - 1;
                }
                timer.Stop();
                A.Activated = false;
                B.Activated = false;
                C.Activated = false;
                start.Activated = true;
            }
            if (C.Clicked)
            {
                if (Antwoord == 3)
                {
                    Score = Score + (1000 + timer_int * 100);
                }
                else
                {
                    levens = levens - 1;
                }
                timer.Stop();
                A.Activated = false;
                B.Activated = false;
                C.Activated = false;
                start.Activated = true;
            }

            if (start.Clicked)
            {
                A.Activated = true;
                B.Activated = true;
                C.Activated = true;

                timer_int = 30;
                timer.Start();
                start.Activated = false;

            }
        }
    }
}
