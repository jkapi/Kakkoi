using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;
using Microsoft.Xna.Framework.Audio;

namespace Game1.Minigames.QuizAfvalRace
{ 
    public partial class Form1 : Form
    {
        int ronde = 1;
        int Timer = 30;
        DialogResult messageFout;
        Timer msgTimer;
        int Pressed = 0;
        int Score;
        int Levens = 3;
        string[] vragen;
        string Vraag1;
        int Min = 1;
        int Max = 3;
    
         
        int Antwoord  = 3;


        int formheight;
        int formwidth;

        private Rectangle resolution;

        /// <summary>
        /// 
        /// </summary>
        public string filepath = @"C:\Users\Nickv\Desktop\HTML\HTML\sss\SITE\Nieuwe map\Websites\Proftaak S2\S1\Fuck friday\Kakoi\Game1\Minigames\QuizAfvalRace\Quiz_File.txt";

        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {

            InitializeComponent();
            resolution = Screen.PrimaryScreen.Bounds;
            Height = resolution.Height;
            Width = resolution.Width;
            Point pointerloc = new Point((resolution.Width / 2) - (panel1.Width / 2), (resolution.Height / 2) - (panel1.Height / 2));

            panel1.Location = pointerloc;

            Sounds achtergroundSound = new Sounds(@"C:\Users\Nickv\Desktop\HTML\HTML\sss\SITE\Nieuwe map\Websites\Proftaak S2\S1\OIM\Wave.wav");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            VraagBox.Enabled = false;
            AntwoordA.Enabled = false;
            AntwoordB.Enabled = false;
            AntwoordC.Enabled = false;
            Start.Enabled = false;
            Start.Visible = false;
            VraagBox.Text = "Wat is juist : Wat is 1 + 1 ";
            AntwoordA.Text = "21";
            AntwoordB.Text = "69";
            AntwoordC.Text = "2";
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// 
        public void End()
        {
            MessageBox.Show("Eind Score:" + " " + Score);
            this.Hide();
        }
        public void _Quiz_Invoer(string Goed, string antwoordA, string antwoordB,string antwoordC,string Vraag)
        {
       

            if (Goed == "A")
            {
                Antwoord = 1;

            }

            if (Goed == "B")
            {
                Antwoord = 2;
            }

            if (Goed == "C")
            {
                Antwoord = 3;
            }

            VraagBox.Text = Vraag;
            AntwoordA.Text = antwoordA;
            AntwoordB.Text = antwoordB;
            AntwoordC.Text = antwoordC;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// 
        /// 
        /// 
        public void Quiz()
        {
            List<string> lines = File.ReadAllLines(filepath).ToList();
            Random random = new Random();
            if (Pressed > 0 && Pressed < 8)
            {
                int result = random.Next(Min, Max);
                Vraag1 = lines[result];

                string[] Text = Vraag1.Split(',');

                string Vraag = Text[0];
                string antwoordA = Text[1];
                string antwoordB = Text[2];
                string antwoordC = Text[3];
                string Goed = Text[4];

                _Quiz_Invoer(Goed, antwoordA, antwoordB, antwoordC, Vraag);
            }

            if (Pressed > 7)
            {
                Timer = 30;
                //TimerLabel.Text = "00:30";
                TimerLabel.Text = Timer.ToString();
                Start.Text = "Finish";
                Start.Enabled = true;
                Start.Visible = true;
                MessageBox.Show("Einde Quiz, Final Score:" + " " + Score);
            }
        }

    
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Timer < 31)
            {
                Timer = Timer - 1;
                if (Timer < 10)
                {
                    TimerLabel.Text = "00:0" + Timer.ToString();
                }
                else
                {
                    TimerLabel.Text = "00:" + Timer.ToString();
                }
                if (Timer < 1)
                {
                    Start.Text = "Verder";
                    Start.Enabled = true;
                    Start.Visible = true;
                    timer1.Enabled = false;
                    //Levens = Levens - 1;
                    //LevensLabel.Text = "Levens:" + " " + Levens;
                }

                if (Timer == 0)
                {
                    KeuzeA.Enabled = false;
                    KeuzeB.Enabled = false;
                    KeuzeC.Enabled = false;
                    if (Pressed > 0 && Levens > 0)
                    {
                        Levens = Levens - 1;
                        LevensLabel.Text = "Levens:" + " " + Levens;
                    }
                }
            }
 
            if(Pressed > 7)
            {
                Timer = 30;
                //TimerLabel.Text = "00:30";
                TimerLabel.Text = Timer.ToString();
                Start.Text = "Finish";
                Start.Enabled = true;
                Start.Visible = true;
               MessageBox.Show("Einde Quiz, Final Score:" + " " + Score);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start.Enabled = true;
            Start.Visible = false;
           
            Timer = 30;
            Pressed = Pressed + 1;
            KeuzeA.Enabled = true;
            KeuzeB.Enabled = true;
            KeuzeC.Enabled = true;

            Box1.Visible = true;
            Box2.Visible = true;
            Box3.Visible = true;
            KeuzeA_Goed.Visible = false;
            KeuzeA_Fout.Visible = false;
            KeuzeB_Goed.Visible = false;
            KeuzeB_Fout.Visible = false;
            KeuzeC_Goed.Visible = false;
            C_Fout.Visible = false;
            
            if (Pressed > 0 && Pressed < 7)
            {
                Min = Min + 3;
                Max = Max + 3;
            }
            
            //MessageBox.Show(Convert.ToString(Pressed));


       

            if(Pressed > 7 )
            {
                TimerLabel.Text = "00:30";
                Start.Text = "Finish";
                Start.Enabled = true;
                Start.Visible = true;
                KeuzeA.Enabled = false;
                KeuzeB.Enabled = false;
                KeuzeC.Enabled = false;
            }
            else
            {
                if (Levens > 0)
                {
                    Quiz();
                    timer1.Start();
                }
                
            }

            if (Levens == 0)
            {
                Start.Text = "Finish";
                KeuzeA.Enabled = false;
                KeuzeB.Enabled = false;
                KeuzeC.Enabled = false;
                
            }

            if (Start.Text == "Finish")
            {
                End();
            }
        }

        private void KeuzeC_Click(object sender, EventArgs e)
        {

            if(Antwoord == 3)
            {
                timer1.Stop();
                Score = Score + 1000 + (50 * Timer);
                if (Pressed > 0)
                {
                    Box3.Visible = false;
                    KeuzeC_Goed.Visible = true;
                }
            }
            else
            {
                timer1.Stop();

                msgTimer = new Timer();
                msgTimer.Interval = 2000;
                msgTimer.Enabled = true;
                msgTimer.Tick += new EventHandler(msgTimerTick);
                messageFout = MessageBox.Show("Fout");

                if (Pressed > 0 && Levens > -1)
                {
                    Box3.Visible = false;
                    C_Fout.Visible = true;
                    Levens = Levens - 1;
                    LevensLabel.Text = "Levens:" + " " + Levens;
                }
            }


            if (Pressed == 0)
            {
                Score = 0;
                MessageBox.Show("Goed zo klik op Start om te beginnen");
                Start.Enabled = true;
                Start.Visible = true;

            }

            if(Pressed > 0)
            {
                Start.Enabled = true;
                Start.Visible = true;
                

            }
            timer1.Stop();
            ScoreLabel.Text = "Score:" + " " + Score;

            KeuzeA.Enabled = false;
            KeuzeB.Enabled = false;
            KeuzeC.Enabled = false;

        }

        private void KeuzeB_Click(object sender, EventArgs e)
        {
            if(Antwoord == 2)
            {
                timer1.Stop();
                Score = Score + 1000 + (50 * Timer);
                if (Pressed > 0)
                {
                    Box2.Visible = false;
                    KeuzeB_Goed.Visible = true;
                }
            }
            else
            {
                timer1.Stop();

                msgTimer = new Timer();
                msgTimer.Interval = 2000;
                msgTimer.Enabled = true;
                msgTimer.Tick += new EventHandler(msgTimerTick);
                messageFout = MessageBox.Show("Fout");

                if (Pressed > 0 && Levens > -1)
                {
                    Box2.Visible = false;
                    KeuzeB_Fout.Visible = true;
                    Levens = Levens - 1;
                    LevensLabel.Text = "Levens:" + " " + Levens;
                }
            }

            if (Pressed > 0)
            {
                Start.Enabled = true;
                Start.Visible = true;
                KeuzeA.Enabled = false;
                KeuzeB.Enabled = false;
                KeuzeC.Enabled = false;
     

            }
            timer1.Stop();
            ScoreLabel.Text = "Score:" + " " + Score;
         
        }
  
        void msgTimerTick(object sender, EventArgs e)
        {
            SendKeys.Send("{ESC}");
            msgTimer.Enabled = false;
        }

        private void KeuzeA_Click(object sender, EventArgs e)
        {
           if(Antwoord == 1)
            {
                timer1.Stop();
                Score = Score + 1000 + (50 * Timer);
                Levens = 3;
                LevensLabel.Text = "Levens:" + " " + Levens;
                if (Pressed > 0)
                {
                    Box1.Visible = false;
                    KeuzeA_Goed.Visible = true;
                }
            }
            else
            {
                timer1.Stop();

                msgTimer = new Timer();
                msgTimer.Interval = 2000;
                msgTimer.Enabled = true;
                msgTimer.Tick += new EventHandler(msgTimerTick);
                messageFout = MessageBox.Show("Fout");
              
                if (Pressed > 0 && Levens > -1)
                {
                    Box1.Visible = false;
                    KeuzeA_Fout.Visible = true;
                    Levens = Levens - 1;
                    LevensLabel.Text = "Levens:" + " " + Levens;
                    
                }
            }

            if (Pressed > 0)
            {
                Start.Enabled = true;
                Start.Visible = true;
                KeuzeA.Enabled = false;
                KeuzeB.Enabled = false;
                KeuzeC.Enabled = false;
   

            }
            timer1.Stop();
            ScoreLabel.Text = "Score:" + " " + Score;
          


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
