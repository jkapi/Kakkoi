using Microsoft.Xna.Framework;
using StrangerCade.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Minigames.ClimbTheMountain
{
    class ClimbTheMountain : Room
    {
        //--DESIGN--//
        public Rectangle playField { get; private set; }
        public Rectangle letterField { get; private set; }
        public int widthLengthBlock { get; private set; }
        public int heightLengthBlock { get; private set; }

        //--GAME--//
        public List<string> queueOfLetters { get; private set; }
        public List<string> listOfLetters { get; private set; }

        public override void Initialize()
        {
            listOfLetters = new List<string>
            {
                "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
                "0","1","2","3","4","5","6","7","8","9"
            };
            queueOfLetters = new List<string>();
            Vector2 field = new Vector2(Graphics.PreferredBackBufferWidth / 4, Graphics.PreferredBackBufferHeight);
            playField = new Rectangle((Graphics.PreferredBackBufferWidth / 2) - ((int)field.X / 2), 0, (int)field.X, (int)field.Y);

            Vector2 field2 = new Vector2(field.X/2, field.Y);
            letterField = new Rectangle(Graphics.PreferredBackBufferWidth / 2 - ((int)field2.X / 2), 0, (int)field2.X, (int)field2.Y);
            widthLengthBlock = letterField.Width / 2;
            heightLengthBlock = letterField.Height / 10;
            InitPlayground();
        }
        public override void Update()
        {
            if (queueOfLetters.Count <= 0 | queueOfLetters.Count < 21)
            {
                queueOfLetters.Add(GetNextRandomLetter());
            }
        }
        public override void Draw()
        {
            View.DrawRectangle(playField, true, Color.Black);
            View.DrawRectangle(letterField, true, Color.Black);
            foreach (Block block in Block.totalBlocks)
            {
                View.DrawRectangle(block.rectangle, true, Color.Yellow);
            }
            
        }
        private string GetNextRandomLetter()
        {
            Random randomNum = new Random();
            string letter = listOfLetters[randomNum.Next(0, listOfLetters.Count-1)];
            return letter;
        }

        public void InitPlayground()
        {

            int even = (letterField.Height / 10);
            int oneven = (letterField.Height / 10) * 2;
            int value = 0;
            for (int X = letterField.X; X < (letterField.X+letterField.Width); X = X + letterField.Width/2)
            {
                value = (X == letterField.X / 2) ? even : oneven;
                for (int Y = letterField.Y; Y < (letterField.Y+letterField.Height); Y = Y + value)
                {   
                    Vector2 startPos = new Vector2(X, Y);
                    Rectangle blockRec = new Rectangle(X,Y,widthLengthBlock,heightLengthBlock);
                    Block currentBlock = new Block(blockRec);
                    Block.totalBlocks.Add(currentBlock);
                }
            }
        }
    }
}
