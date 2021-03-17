using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

namespace Seshat
{
    public static class Logger
    {
        private static StreamWriter _logFile;

        public enum LogLevel
        {
            Error,
            Warn,
            Info,
            Debug
        }

        public static void Log(LogLevel level, string tag, string message)
        {
            
            _logFile.Write("[");
            _logFile.Write(LevelToString(level));
            _logFile.Write("] [");
            _logFile.Write(tag);
            _logFile.Write("] ");
            _logFile.WriteLine(message);
            _logFile.Flush();
        }

        public static void LogException(this Exception e)
        {
            _logFile.Write(e.GetType().FullName);
            _logFile.Write(": ");
            _logFile.WriteLine(e.Message);

            _logFile.WriteLine(e.StackTrace);
            _logFile.Flush();
        }

        public static void Error(string tag, string message)
            => Log(LogLevel.Error, tag, message);
        public static void Warn(string tag, string message)
            => Log(LogLevel.Warn, tag, message);
        public static void Info(string tag, string message)
            => Log(LogLevel.Info, tag, message);
        public static void Debug(string tag, string message)
            => Log(LogLevel.Debug, tag, message);

        internal static void OpenLogFile()
            => OpenLogFile(Path.Combine(Path.GetDirectoryName(Application.consoleLogPath), "ModLog.log"));

        internal static void OpenLogFile(string file)
        {
            FileStream logFile = new FileStream(file, FileMode.Create);
            _logFile = new StreamWriter(logFile);
        }

        internal static void CloseLogFile()
        {
            _logFile.Dispose();
            _logFile = null;
        }

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
