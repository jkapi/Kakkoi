using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Minigames.Quiz
{
    class Vraag
    {
        public string AntwoordA { get; private set; }
        public string AntwoordB { get; private set; }
        public string AntwoordC { get; private set; }
        public string Vraagje { get; private set; }
        public string Antwoord { get; private set; }

        static string path;
        public static List<String> vragenList { get; private set; }
        public static List<Vraag> vragen { get; private set; }
        private static Random random { get; set; }

        public Vraag(string vraagje, string antwoordA, string antwoordB, string antwoordC, string antwoord)
        {
            AntwoordA = antwoordA;
            AntwoordB = antwoordB;
            AntwoordC = antwoordC;
            Vraagje = vraagje;
            Antwoord = antwoord;
        }
        static Vraag()
        {
            path = @"C:\Users\Nickv\Desktop\HTML\HTML\sss\SITE\Nieuwe map\Websites\Proftaak S2\S1\Fuck friday\Kakoi\Game1\Minigames\Quiz\Files\Quiz_File.txt";
            random = new Random();
            vragenList = new List<string>();
            vragen = new List<Vraag>();

            vragenList = File.ReadAllLines(path).ToList();

           foreach (string vraag in vragenList)
           {
                string[] Text = vraag.Split(',');
                vragen.Add(new Vraag(Text[0], Text[1], Text[2], Text[3], Text[4]));
            };
        }

        public static Vraag GetVraag()
        {
          


                int randomNum = random.Next(0, vragen.Count - 1);
                Vraag vraag = vragen[randomNum];
                vragen.RemoveAt(randomNum);
                return vraag;
            

        }


    }
}
