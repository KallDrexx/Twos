using System;
using System.IO;
using Twos.Models;

namespace Twos.Processors
{
    public class ActionLogWriter : IDisposable
    {
        private readonly FileStream _logFile;

        public ActionLogWriter(string logFileName, int gameSeed)
        {
            _logFile = File.Open(logFileName, FileMode.Create, FileAccess.Write);

            var seedBytes = BitConverter.GetBytes(gameSeed);
            _logFile.Write(seedBytes, 0, seedBytes.Length);
        }

        public void LogAction(GameAction action)
        {
            var actionBytes = BitConverter.GetBytes((int) action);
            _logFile.Write(actionBytes, 0, actionBytes.Length);
        }

        public void Dispose()
        {
            _logFile.Dispose();
        }
    }
}
