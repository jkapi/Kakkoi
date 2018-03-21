using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Minigames.QuizAfvalRace
{
    public class Sounds
    {
        SoundPlayer player = new SoundPlayer();

        public Sounds(string file)
        {
            player.Stop();
            player.SoundLocation = file;
            player.Play();
        }
    }
}
