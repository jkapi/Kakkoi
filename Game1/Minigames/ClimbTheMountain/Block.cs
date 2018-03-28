using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrangerCade.Framework;

namespace Game1.Minigames.ClimbTheMountain
{
    class Block
    {
        //public Vector2 position { get; private set; }
        //public int width { get; private set; }
        //public int height{ get; private set; }
        public Rectangle rectangle { get; private set; }
        public string letter { get; set; }

        static public Random randomNum { get; private set; } = new Random();
        
        static public Block[] totalBlocks { get; private set; } = new Block[10];

        static public SpriteFont Arial;

        static public List<string> listOfLetters = new List<string>
            {
                "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
                "0","1","2","3","4","5","6","7","8","9"
            };
            
        public Block(Rectangle Rectangle) 
        {
            rectangle = Rectangle;
            letter = listOfLetters[randomNum.Next(0, listOfLetters.Count - 1)];
        }

        public void drawBlock()
        {
            //DrawText(Arial, letter, new Vector2(0, 0));
            //Arial2.DrawString(Arial,"A",,Color.Black)
        }

        static public void ReverseArray()
        {
            totalBlocks = totalBlocks.Reverse().ToArray();
        }

    }
}
