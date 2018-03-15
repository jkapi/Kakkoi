using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game1.Minigames.QuizAfvalRace
{ 
    public partial class Form1 : Form
    {
        int ronde = 1;
        int Timer = 30;
        int Pressed = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void _Quiz()
        {
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(Timer > 10)
            {
                Timer = Timer - 1;
                label1.Text = "00:" + Timer;

            }

            if(Timer == 10)
            {
                Timer = Timer - 1;
                label1.Text = "00:" + Timer;
            }

            if(Timer < 10 && Timer > 0)
            {
                Timer = Timer - 1;
                label1.Text = "00:0" +Timer;
            }

            if(Timer == 0)
            {
                label1.Text = "00:00";
                Start.Text = "Verder";
                Start.Enabled = true;
                Start.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start.Enabled = true;
            Start.Visible = false;
            timer1.Start();
            Timer = 30;

        }
    }
}
