using Microsoft.Xna.Framework;
using StrangerCade.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Game1.Minigames.DontTapWhite
{
    class PlayerDTW : GameObject
    {
        //player id
        public int id { get; private set; }
        //time left to click
        public int time { get; private set; }
        public int timeLeftToClick { get; private set; }
        //score of the player
        public int score { get; private set; }
        //0 = loading screen
        //1 = play screen
        //2 = end screen
        public int stateOfGame { get; private set; }
        //lives of the player
        public int life { get; private set; }
        public Timer timer { get; private set; }

        private static int idCounter = 0;
        public PlayerDTW() : base(Vector2.Zero)
        {
            time = 30;
            timeLeftToClick = 10;
            score = 0;
            stateOfGame = 0;
            life = 1;
            timer = new Timer();
            timer.Elapsed += OnTimedEvent;
            timer.Enabled = true;
            timer.Interval = 1000;
            id = idCounter;

            idCounter++;
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

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (time < 31 & time > -1 & stateOfGame == 1)
            {
                time -= 1;
            }
            else
            {
                if (time < 1 | time > 30)
                {
                    stateOfGame = 2;
                }
            }

            if (timeLeftToClick > -1 & timeLeftToClick < 11 & stateOfGame == 1)
            {
                timeLeftToClick -= 1;
            }
            else
            {
                if (timeLeftToClick < 1 | timeLeftToClick > 10)
                {
                    stateOfGame = 2;
                }
            }

            if (life < 1)
            {
                stateOfGame = 2;
            }

        }

        public void ScoreIncrement()
        {
            score += 1;
        }

        public void StartScreen()
        {
            stateOfGame = 0;
        }
        public void PlayScreen()
        {
            stateOfGame = 1;
        }
        public void Endscreen()
        {
            stateOfGame = 2;
        }

        public void PlayerDead()
        {
            life = 0;
        }

        public void ResetTimerLeftToClick()
        {
            timeLeftToClick = 10;
        }


    }
}
