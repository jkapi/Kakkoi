using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game1.Minigames.BKE;
using Game1.Minigames.QuizAfvalRace;

namespace Game1.Minigames.BKE
{
    public partial class Form1 : Form
    {
        public GFX engine;
        public Board theBoard;
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics toPass = panel1.CreateGraphics();
            engine = new GFX(toPass);

            theBoard = new Board();
            theBoard.initBoard();
            RefreshLabel();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Point Mouse = Cursor.Position;
            Mouse = panel1.PointToClient(Mouse);
            theBoard.detectHit(Mouse);
            RefreshLabel();
        }

        private void Restart_Click(object sender, EventArgs e)
        {
            theBoard.reset();
            GFX.setUpCanvas();
        }

     

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        public void RefreshLabel()
        {
            string NewTekst = "It is";

            if (theBoard.GetPlayersTurn() == Board.X)
            {
                NewTekst = " It is X's turn \n";
            }
            else
            {
                NewTekst = " It is O's turn \n ";
            }

            NewTekst += "X has won " + theBoard.GetXwins() + " times. \n ";
            NewTekst += "O has won " + theBoard.GetOwins() + " times. ";

            label1.Text = NewTekst;
        } 
    }
}
