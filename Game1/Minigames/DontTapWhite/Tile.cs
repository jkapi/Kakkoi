using Microsoft.Xna.Framework;
using StrangerCade.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Minigames.DontTapWhite
{
    class Tile : GameObject
    {

        public Rectangle tile { get; private set; }
        public Color color { get; set; }
        public bool outline { get; set; }
        //Which number of the tile
        public int positionTile { get; private set; }
        //X and Y position of the tile
        public Vector2 position { get; private set; }

        static public Tile[] totalTiles;

        //Generate random position
        public Tile(Rectangle Tile, Color Color, bool Outline, int Position) : base(Vector2.Zero)
        {
            int generatePosition = GetRandomTilePos();
            foreach (Tile tile in totalTiles)
            {
                if (tile.positionTile == generatePosition)
                {
                    if (tile.color != Color.Black)
                    {
                        this.tile = Tile;
                        this.color = Color;
                        this.outline = Outline;
                        totalTiles[GetRandomTilePos()] = this;
                    }
                }
            }
        }

        //Assign position
        public Tile(Rectangle Tile, Color Color, bool Outline, Vector2 Position, int PositionTile) : base(Vector2.Zero)
        {
            if (PositionTile > totalTiles.Length)
            {
                Console.WriteLine("Position out of range");
                return;
            }
            tile = Tile;
            color = Color;
            outline = Outline;
            positionTile = PositionTile;
            position = Position;
            totalTiles[positionTile] = this;
        }

        public void DrawTile(Tile aTile)
        {
            View.DrawRectangle(aTile.tile, aTile.outline, aTile.color);
        }

        static public int GetRandomTilePos()
        {
            Random tileNum = new Random();
            return tileNum.Next(0, totalTiles.GetLength(0));
        }

        static public int GetRandomTilePos(int start, int end)
        {
            Random tileNum = new Random();
            return tileNum.Next(start, end);
        }

        static public void InitTotalTiles(int TilesCount)
        {
            totalTiles = new Tile[TilesCount];
        }
    }
}
