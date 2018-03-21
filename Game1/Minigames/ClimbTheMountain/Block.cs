using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Minigames.ClimbTheMountain
{
    class Block
    {
        //public Vector2 position { get; private set; }
        //public int width { get; private set; }
        //public int height{ get; private set; }
        public Rectangle rectangle { get; private set; }
        static public List<Block> totalBlocks { get; private set; } = new List<Block>();

        public Block(Rectangle Rectangle)
        {
            rectangle = Rectangle;
        }
        
    }
}
