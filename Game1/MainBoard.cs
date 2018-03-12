using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrangerCade.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Game1
{
    class MainBoard : GameObject
    {
        Sprite chips;

        public MainBoard() : base(Vector2.Zero){}

        int[,] board = new int[8, 8];

        int currentplayer = 0;

        public override void Initialize()
        {
            chips = new Sprite(Room.Content.Load<Texture2D>("chips"));
            Sprite = chips;
            SpriteOrigin = chips.Center;
            for (int i = 0; i < 64; i++)
            {
                board[i % 8, i / 8] = -1;
            }

            board[3, 3] = 0;
            board[3, 4] = 1;
            board[4, 3] = 2;
            board[4, 4] = 3;
        }

        public override void Update()
        {
            Position = Mouse.Position / View.Scale;
            Position.X = (float)Math.Round(Math.Min(Math.Max(0,Position.X / 128),7)) * 128 + 96;
            Position.Y = (float)Math.Round(Math.Min(Math.Max(0, Position.Y / 128), 7)) * 128 + 96;
            SpriteIndex = currentplayer;
            if (Mouse.CheckPressed(MouseButtons.Left) )
            {
                int x = (int)(Position.X - 32) / 128;
                int y = (int)(Position.Y - 32) / 128;
                if (board[x, y] == -1 || board[x,y] > 3)
                {
                    if (CanPlace(x,y))
                    {
                        board[x, y] = currentplayer;
                        DoMove(x, y, currentplayer);
                        currentplayer++;
                    }
                }
                else
                {
                    board[x, y] += 4;
                    currentplayer++;
                }
                if (currentplayer == 4)
                {
                    currentplayer = 0;
                }
            }
        }

        public override void Draw()
        {
            SpriteRotation++;
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (board[x, y] != -1)
                    {
                        View.DrawSprite(chips, board[x, y], new Vector2(x * 128, y * 128) + new Vector2(32));
                    }
                }
                View.DrawLine(new Vector2(0, y * 128) + new Vector2(32), new Vector2(1024, y * 128) + new Vector2(32));
                View.DrawLine(new Vector2(y * 128, 0) + new Vector2(32), new Vector2(y * 128, 1024) + new Vector2(32));
            }
            View.DrawLine(new Vector2(0, 1024) + new Vector2(32), new Vector2(1024, 1024) + new Vector2(32));
            View.DrawLine(new Vector2(1024, 0) + new Vector2(32), new Vector2(1024, 1024) + new Vector2(32));
        }
        
        void DoMove(int px, int py, int pc)
        {
            List<Point> directions = new List<Point>() {
                new Point(-1,-1), new Point(0,-1), new Point(1,-1),
                new Point(-1, 0), /*   niets    */ new Point(1, 0),
                new Point(-1, 1), new Point(0, 1), new Point(1, 1)
            };
            foreach (Point direction in directions)
            {
                int length = CheckDirection(new Point(px, py), direction, pc);
                if (length > 0)
                {
                    for (int i = 1; i <= length; i++)
                    {
                        board[px + (direction.X * i), py + (direction.Y * i)] = pc;
                    }
                }
            }

        }

        int CheckDirection(Point lc, Point direction, int pc)
        {
            int i = 1;
            int steps = 0;
            lc += direction;
            while (lc.X >= 0 && lc.Y >= 0 && lc.X < 8 && lc.Y < 8 && i > 0)
            {
                if (board[lc.X, lc.Y] == pc || board[lc.X, lc.Y] == pc + 4)
                {
                    steps = i;
                    i = -1;
                }
                if (board[lc.X, lc.Y] == -1)
                {
                    i = -1;
                }
                i++;
                lc += direction;
            }
            return steps;
        }

        bool CanPlace(int x, int y)
        {
            return GetBoardPlaceFilled(x - 1, y - 1) || GetBoardPlaceFilled(x, y - 1) || GetBoardPlaceFilled(x + 1, y - 1) ||
                   GetBoardPlaceFilled(x - 1, y)     || GetBoardPlaceFilled(x, y)     || GetBoardPlaceFilled(x + 1, y    ) ||
                   GetBoardPlaceFilled(x - 1, y + 1) || GetBoardPlaceFilled(x, y + 1) || GetBoardPlaceFilled(x + 1, y + 1);
        }

        int GetBoardPlace(int x, int y)
        {
            if (x < 0 || x > 7 || y < 0 || y > 7)
            {
                return -1;
            }
            else
            {
                return board[x, y];
            }
        }

        bool GetBoardPlaceFilled(int x, int y)
        {
            if (x < 0 || x > 7 || y < 0 || y > 7)
            {
                return false;
            }
            else
            {
                return board[x, y] != -1;
            }
        }
    }
}
