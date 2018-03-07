using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Minigames.DontTapWhite
{
    class Game : GameObject
    {

        int[,] grid = new int[8,8];
        int gridDimensionLength;

        public Game() : base(Vector2.Zero)
        {

            //grid = getRandomGrid();
            gridDimensionLength = grid.GetLength(0);
            
        }

        public int[,] getRandomGrid()
        {
            List<int[,]> listGrids = new List<int[,]>();
            Random getListGridsNum = new Random();

            //Initialize grids
            for (int i = 4; i < 9; i+=2)
            {
                listGrids.Add(new int[i, i]);
            }

            //Get a random grid back
            return listGrids[getListGridsNum.Next(0, listGrids.Count)];

        }

        public override void Draw()
        {
            //The main grid
            View.DrawColor = Color.Black;
            Rectangle rec = new Rectangle(700,0, Room.Graphics.PreferredBackBufferWidth/2, Room.Graphics.PreferredBackBufferHeight);
            //new Point(rec.X + rec.Width / 2, rec.Bottom);
            View.DrawRectangle(rec,true);

            //Fill the grid with rectangles
            for (int y = 0; y < gridDimensionLength; y++)
            {
                for (int x = 0; x < gridDimensionLength; x++)
                {
                    int widthLengthTile = rec.Width / gridDimensionLength;
                    int heightLengthTile = rec.Height / gridDimensionLength;
                    float startX = (widthLengthTile * x) + rec.X;
                    float startY = (heightLengthTile * y) + rec.Y;
                    Rectangle tileRec = new Rectangle(Convert.ToInt32(startX), Convert.ToInt32(startY), widthLengthTile, heightLengthTile);
                    View.DrawRectangle(tileRec, true, Color.Yellow);
                }
            }
        }
    }
}
