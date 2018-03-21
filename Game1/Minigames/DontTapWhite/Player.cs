using Microsoft.Xna.Framework;
using StrangerCade.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Minigames.DontTapWhite
{
    class Player : GameObject
    {
        //time left to click
        public int time { get; private set; }
        //score of the player
        public int score { get; private set; }
        //0 = loading screen
        //1 = play screen
        //2 = end screen
        public int stateOfGame { get; private set; }
        //lives of the player
        public int life { get; private set; }

        public Player() : base(Vector2.Zero)
        {
            time = 10;
            score = 0;
            stateOfGame = 0;
            life = 1;
        }

        public override void Initialize() 
        {

        }

        public override void Update()
        {
            if (stateOfGame == 0)
            {

            }
            if (stateOfGame == 1)
            {

            }
            if (stateOfGame == 2)
            {

            }
        }

    }
}
