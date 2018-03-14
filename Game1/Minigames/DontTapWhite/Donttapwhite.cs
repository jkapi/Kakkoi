using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using StrangerCade.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game1.Minigames.DontTapWhite
{
    class Donttapwhite : Room 
    {
        int[,] grid;
        public int gridDimensionLengthX { get; private set; }
        public int gridDimensionLengthY { get; private set; }

        List<Tile> whiteTiles = new List<Tile>();


        //Field
        public Rectangle rec { get; private set; }

        //Properties of the tile
        public int widthLengthTile { get; private set; }
        public int heightLengthTile { get; private set; }

        //Spawn delay Click tile
        int delay;

        Vector2 mousePos;

        public override void Initialize()
        {
            grid = getRandomGrid();
            gridDimensionLengthX = grid.GetLength(0);
            gridDimensionLengthY = grid.GetLength(1);
            rec = new Rectangle(Graphics.PreferredBackBufferWidth / 2 - Graphics.PreferredBackBufferWidth / 4, 0, Graphics.PreferredBackBufferWidth / 2, Graphics.PreferredBackBufferHeight);
            widthLengthTile = rec.Width / gridDimensionLengthX;
            heightLengthTile = rec.Height / gridDimensionLengthY;
            Tile.InitTotalTiles(grid.GetLength(0) * grid.GetLength(1));
            InitPlayground();
            delay = 20;
        }

        public int[,] getRandomGrid()
        {
            List<int[,]> listGrids = new List<int[,]>();
            Random getListGridsNum = new Random();

            //Initialize multiple grids in listGrids
            for (int i = 4; i < 9; i += 2)
            {
                listGrids.Add(new int[i, i]);
            }

            //Get a random grid back
            return listGrids[getListGridsNum.Next(0, listGrids.Count)];

        }

        public override void Update()
        {
            if (Mouse.CheckPressed(MouseButtons.Left))
            {
                mousePos = Mouse.Position;
            }
            foreach (Tile aTile in Tile.totalTiles)
            {
                if (mousePos.X > aTile.position.X & mousePos.X < aTile.position.X + aTile.tile.Width & mousePos.Y > aTile.position.Y & mousePos.Y < aTile.position.Y + aTile.tile.Height)
                {
                    aTile.color = Color.Gray;
                    aTile.outline = true;

                }
            }
        }
        public override void Draw()
        {
            //draw the grid
            foreach (Tile aTile in Tile.totalTiles)
            {
                View.DrawRectangle(aTile.tile, aTile.outline, aTile.color);
            }
            //spawn the click tiles.
            if (delay < 0)
            {
                foreach (Tile aTile in Tile.totalTiles)
                {
                    if (aTile.color == Color.Gray)
                    {
                        whiteTiles.Add(aTile);
                    }
                }

                if (whiteTiles.Count > 0)
                {
                    Tile currentTile = whiteTiles[Tile.GetRandomTilePos(0, whiteTiles.Count)];
                    foreach (Tile aTile in Tile.totalTiles)
                    {
                        if (aTile.positionTile == currentTile.positionTile)
                        {
                            aTile.outline = false;
                            aTile.color = Color.Black;
                        }
                    }
                }
                whiteTiles.Clear();
                delay = 20;
            }
            else
            {
                delay--;
            }
        }

        public void InitPlayground()
        {
            //The main grid
            //View.DrawColor = Color.Black;
            //new Point(rec.X + rec.Width / 2, rec.Bottom);
            //View.DrawRectangle(rec, true);
            int widthLengthTile = rec.Width / gridDimensionLengthX;
            int heightLengthTile = rec.Height / gridDimensionLengthY;
            //Fill the grid with rectangles
            //for (int y = 1; y < gridDimensionLength + 1; y++)
            //{
            //    for (int x = 1; x < gridDimensionLength + 1; x++)
            //    {
            //        float startX = (widthLengthTile * x) + rec.X;
            //        float startY = (heightLengthTile * y) + rec.Y;
            //        Rectangle tileRec = new Rectangle(Convert.ToInt32(startX), Convert.ToInt32(startY), widthLengthTile, heightLengthTile);
            //        //Tile currentTile = new Tile(tileRec, Color.Yellow, true, grid.GetLength(0));
            //        //currentTile.DrawTile();

            //        View.DrawRectangle(tileRec, true, Color.Yellow);
            //    }
            //}
            int countX = 0;
            int countY = 0;
            for (int i = 0; i < grid.GetLength(0) * grid.GetLength(1); i++)
            {
                Vector2 startPos = new Vector2((widthLengthTile * countX) + rec.X, (heightLengthTile * countY) + rec.Y);
                Rectangle tileRec = new Rectangle(Convert.ToInt32(startPos.X), Convert.ToInt32(startPos.Y), widthLengthTile, heightLengthTile);
                Tile currentTile = new Tile(tileRec, Color.Gray, true, startPos, i);

                if (countX < grid.GetLength(0) - 1)
                {
                    countX++;
                }
                else
                {
                    if (countY < grid.GetLength(1) - 1)
                    {
                        countY++;
                        countX = 0;
                    }
                    else
                    {
                        return;
                    }

                }
            }
        }

        //public void DrawClickTile(Tile Tile)
        //{
        //    View.DrawRectangle(Tile.tile, false, Color.Black);
        //    Tile.color = Color.Black;
        //    Tile.outline = false;
        //}
    }
}
