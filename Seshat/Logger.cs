using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seshat
{
    public static class Logger
    {
        public enum LogLevel
        {
            Error,
            Warn,
            Info,
            Debug
        }

        public static void Log(LogLevel level, string tag, string message)
        {
            Console.Write("[");
            Console.Write(LevelToString(level));
            Console.Write("] [");
            Console.Write(tag);
            Console.Write("] ");
            Console.WriteLine(message);
        }

        public static void LogException(this Exception e)
        {
            Console.Write(e.GetType().FullName);
            Console.Write(": ");
            Console.WriteLine(e.Message);

            Console.WriteLine(e.StackTrace);
        }

        public static void Error(string tag, string message)
            => Log(LogLevel.Error, tag, message);
        public static void Warn(string tag, string message)
            => Log(LogLevel.Warn, tag, message);
        public static void Info(string tag, string message)
            => Log(LogLevel.Info, tag, message);
        public static void Debug(string tag, string message)
            => Log(LogLevel.Debug, tag, message);

        private static string LevelToString(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Error:
                    return "ERROR";
                case LogLevel.Warn:
                    return "WARN";
                case LogLevel.Info:
                    return "INFO";
                case LogLevel.Debug:
                    return "DEBUG";
                default:
                    throw new Exception(); // this is unreachable
            }
        }
    }
}
