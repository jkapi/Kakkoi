using StrangerCade.Framework.Multiplayer;
using System;
using System.Windows.Forms;

namespace Game1
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(StrangerCade.Framework.Logger.Exception);
            //using (var game = new Game1())
            //    game.Run();
            Application.Run(new Minigames.QuizAfvalRace.Form1());
            MultiplayerClient.Disconnect();
        }
    }
}
