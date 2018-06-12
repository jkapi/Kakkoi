using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrangerCade.Framework.Multiplayer
{
    class Player
    {
        public float X;
        public float Y;
        public Vector2 Position;

        public float XSpeed;
        public float YSpeed;
        public Vector2 Speed;
        
        public string Name;
        public int Id;

        public int NumInRoom;

        public int Score;

        public Player(int id, string name, float x, float y, float xSpeed, float ySpeed, int num, int score)
        {
            Name = name;
            Id = id;

            X = x;
            Y = y;
            Position = new Vector2(x, y);

            XSpeed = xSpeed;
            YSpeed = ySpeed;
            Speed = new Vector2(xSpeed, ySpeed);

            NumInRoom = num;

            Score = score;
        }
    }
}
