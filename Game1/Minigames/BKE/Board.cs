using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Game1.Minigames.QuizAfvalRace;

namespace Game1.Minigames.BKE
{
    public class Board 
    {
        public int MovesMade = 0;   
        public int Owins = 0;
        public int Xwins = 0;
       

        private Holder [,] holders = new Holder[3,3];
        public const int  X = 0;
        public const int O = 1;
        public const int B = 2;

        public int playersTurn = X;

        public int GetPlayersTurn()
        {
            return playersTurn;
        }
        
        public int GetOwins()
        {
            return Owins;
        }

        public int GetXwins()
        {
            return Xwins;
        }

       

        public void initBoard()
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    // locatie op het krijgt een waarde zodat het de waarde krijgt van leeg
                    holders[x,y] = new Holder();
                    holders[x, y].setValue(B);
                    holders[x, y].setLocation(new Point(x,y));
                }
            }
        }

        public void detectHit(Point loc)
        {

            if (loc.Y <= 500)
            {


                int x = 0;
                int y = 0;

                // wanneer er geklikt wordt binnen waarde dan krijgt het een bepaalde X of Y coordininaat
                if (loc.X < 167)
                {
                    x = 0;
                }
                else if (loc.X > 167 && loc.X < 334)
                {
                    x = 1;
                }
                else if (loc.X > 334)
                {
                    x = 2;
                }

                if (loc.Y < 167)
                {
                    y = 0;
                }
                else if (loc.Y > 167 && loc.Y < 334)
                {
                    y = 1;
                }
                else if (loc.Y > 334 && loc.Y < 500)
                {
                    y = 2;
                }

                MovesMade++;
                // Even = X Oneven  = O
                // Wanneer X een X plaats is O aan de beurt
                if (MovesMade % 2 == 0)
                {
                    // Wanneer er geen X of O op het bord staat waar er geklikt 
                    if (holders[x, y].getValue() != X & holders[x, y].getValue() != O)
                    {
                        GFX.drawX(new Point(x, y));
                        holders[x, y].setValue(X);
                        playersTurn = O;
                    }
                    // wanneer er een rij is van X
                    if (detectRow())
                    {
                        MessageBox.Show("X won!!");
                        Xwins++;
                        reset();
                        GFX.setUpCanvas();
                    }
                    // wwaneer X drie keer wint
                    if (Xwins == 3)
                    {
                        MessageBox.Show("X is the best out of three");
                        Xwins = 0;
                        Owins = 0;

                    }
                    
                }
                else
                {
                    if (holders[x, y].getValue() != X & holders[x, y].getValue() != O)
                    {
                        GFX.drawO(new Point(x, y));
                        holders[x, y].setValue(O);
                        playersTurn = X;
                    }
                    // wanneer er een rij is van O
                    if (detectRow())
                    {
                        MessageBox.Show("O won!!");
                        Owins++;
                        reset();
                        GFX.setUpCanvas();
                    }
                    // wanneer O drie keer wint
                    if (Owins == 3)
                    {
                        MessageBox.Show("O is the best out of three");
                        Owins = 0;
                        Xwins = 0;

                    }

                   
                }
            }

    
       
        }

        public bool detectRow()
        {
            bool isWon = false;

            for (int x = 0; x < 3; x++)
            {
                // zoekt naar een schuine rij van boven links naar beneden
                if (holders[x, 0].getValue() == X && holders[x, 1].getValue() == X && holders[x, 2].getValue() == X)
                {
                    return true;
                }

                if (holders[x, 0].getValue() == O && holders[x, 1].getValue() == O && holders[x, 2].getValue() == O)
                {
                    return true;
                }

               
                switch (x)
                {
                    // zoeken naar horizontale lijnen
                    case 0:
                        if (holders[x,0].getValue() == X && holders[x+1,1].getValue() == X && holders[x+ 2,2].getValue() == X)
                        {
                            return true;
                        }

                        if (holders[x, 0].getValue() == O && holders[x + 1, 1].getValue() == O && holders[x + 2, 2].getValue() == O)
                        {
                            return true;
                        }

                        break;
                    case 2:
                        // schuine rij van onder links naar boven
                        if (holders[x, 0].getValue() == X && holders[x - 1, 1].getValue() == X && holders[x - 2, 2].getValue() == X)
                        {
                            return true;
                        }

                        if (holders[x, 0].getValue() == O && holders[x - 1, 1].getValue() == O && holders[x - 2, 2].getValue() == O)
                        {
                            return true;
                        }
                        break;
                }
            }

            for (int y = 0; y < 3; y++)
            {
                // zoeken naar verticale lijnen
                if (holders[0, y].getValue() == X && holders[1, y].getValue() == X && holders[2, y].getValue() == X)
                {
                    return true;
                }

                if (holders[0, y].getValue() == O && holders[1, y].getValue() == O && holders[2, y].getValue() == O)
                {
                    return true;
                }
            }

            return isWon;
        }

        public void reset()
        {
            // opnieuw het bord laden zodat het leeg is 

            // alle  9 mogelijke vakken om aan te klikken krijgen de waarde van leeg
            // het bord wordt opnieuw getekent met dezelfde lay out
            holders = new Holder[3,3];
            initBoard();
        }
    }

    public class Holder
    {
        private Point location;
        private int value = Board.B;
        // constructor
        public void setLocation(Point p)
        {
            location = p;
        }

        public Point getLocation()
        {
            return location;
        }

        public void setValue(int i)
        {
            value = i;
        }

        public int getValue()
        {
            return value; 
        }
    }
}
