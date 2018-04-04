using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1.Minigames.FollowTheLeader
{
    class AI : PlayerFTL
    {
        private static Random randomNum;

        public AI(Rectangle playField, int movementspeed) : base(5, playField, RandomPositionAI(playField), movementspeed)
        {

        }

        private static Vector2 RandomPositionAI(Rectangle PlayField)
        {
            randomNum = new Random();
            return new Vector2(randomNum.Next(PlayField.X, PlayField.Width+ PlayField.X), randomNum.Next(PlayField.Y, PlayField.Height + PlayField.Y));
        }
    }
}
