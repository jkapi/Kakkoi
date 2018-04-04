using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrangerCade.Framework
{
    class Logger
    {
        public static LogType Handler = LogType.None;
        private static bool initialized = false;
        private static StreamWriter stream;
        public static void WriteLine(string message, LogLevel logLevel = LogLevel.INFO)
        {
#if DEBUG
            Handler = LogType.Console | LogType.File;
#endif
            string msg = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] [" + logLevel.ToString() + "] " + message;
            if (Handler.HasFlag(LogType.Console))
                Debug.WriteLine(msg);
            if (Handler.HasFlag(LogType.File))
            {
                if (!initialized)
                    Initialize();
                stream.WriteLine(msg);
                stream.Flush();
            }
        }

        private static void Initialize()
        {
            stream = new StreamWriter(File.Open(DateTime.Now.ToString("yyyy-MM-dd HHmmss") + ".log", FileMode.Append, FileAccess.Write));
            initialized = true;
        }

        internal static void Exception(object sender, UnhandledExceptionEventArgs e)
        {
            Handler = LogType.File | LogType.Console;
            WriteLine("Unhandled Exception: " + e.ToString(), LogLevel.FATAL);
            Environment.Exit(1);
        }

        public enum LogType
        {
            File, Console, None
        }
    }

    enum LogLevel
    {
        DEBUG, ERROR, FATAL, INFO, TRACE, WARN
    }
}
