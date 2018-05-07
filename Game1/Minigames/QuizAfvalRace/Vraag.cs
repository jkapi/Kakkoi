using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Minigames.QuizAfvalRace
{
    class Vraag
    {
        private List<string> catogorie1 = new List<string>();
        private List<string> catogorie2 = new List<string>();
        private List<string> catogorie3 = new List<string>();
        private List<string> catogorie4 = new List<string>();
        private List<string> catogorie5 = new List<string>();



        public List <string> Catogorie1
        {
            get
            {
                return catogorie1;
            }
        }

        public List<string> Catogorie2
        {
            get
            {
                return catogorie2;
            }
        }

        public List<string> Catogorie3
        {
            get
            {
                return catogorie3;
            }
        }

        public List<string> Catogorie4
        {
            get
            {
                return catogorie4;
            }
        }

        public List<string> Catogorie5
        {
            get
            {
                return catogorie5;
            }
        }


    }
}
