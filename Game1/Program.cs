﻿using StrangerCade.Framework;
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
            Logger.Handler = Logger.LogType.Console | Logger.LogType.File;
#if NICKSSHIZZLE
            //Application.Run(new Minigames.QuizAfvalRace.Form1());
            using (var game = new Game1())
                game.Run();
#else
            using (var game = new Game1())
                game.Run();
#endif
        }
    }
}
