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
    class Tile : GameObject
    {
        public Texture2D sprite;
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

        //Init the totalTiles.
        static public void InitTotalTiles(int TilesCount)
        {
            totalTiles = new Tile[TilesCount];
        }

        //Draws the tile on the screen
        public void DrawTile(Tile aTile)
        {
            View.DrawRectangle(aTile.tile, aTile.outline, aTile.color);
        }
        //Generate a random number between 0 and totalTiles.Count
        static public int GetRandomTilePos()
        {
            Random tileNum = new Random();
            return tileNum.Next(0, totalTiles.GetLength(0));
        }
        //Generate a random number between the given numbers.
        static public int GetRandomTilePos(int start, int end)
        {
            Random tileNum = new Random();
            return tileNum.Next(start, end);
        }

        public void ChangeTilePosition(Tile Tile, int NewPosition)
        {
            foreach (Tile aTile in totalTiles)
            {
                if (aTile.positionTile == Tile.positionTile)
                {
                    aTile.positionTile = NewPosition;
                }
            }
        }

        public override void Draw()
        {
            if (sprite != null)
            {
                View.DrawTextureStretched(sprite, new Vector2(tile.X, tile.Y), new Vector2(250,250));
                
            }
        }
    }
}
