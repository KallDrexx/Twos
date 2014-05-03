using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twos.Models;
using Twos.Processors;

namespace Twos
{
    class Program
    {
        private const string LogOutputDirectory = @"C:\temp";
        private const string LogExtension = ".twos";

        static void Main(string[] args)
        {
            var lastLogFileName = GetLastCreatedLogFilename();
            if (!string.IsNullOrWhiteSpace(lastLogFileName))
            {
                var loggedGame = ActionLogReader.ReadLog(lastLogFileName);
            }

            var output = new OutputProcessor();
            var actionProcessor = new GameActionProcessor(123);

            var state = actionProcessor.GenerateInitialBoard();
            output.DisplayGame(state, actionProcessor.Seed);

            var logFileName = Path.Combine(LogOutputDirectory, DateTime.Now.ToString("yyyyMMddhhmmss") + LogExtension);
            using (var writer = new ActionLogWriter(logFileName, actionProcessor.Seed))
            {
                while (state.Status == GameStatus.InProgress)
                {
                    var action = GetActionFromKeyPress();
                    actionProcessor.RunGameAction(state, action);

                    output.DisplayGame(state, actionProcessor.Seed);
                    writer.LogAction(action);
                }
            }

            Console.ReadLine();
        }

        private static GameAction GetActionFromKeyPress()
        {
            var keyMap = new Dictionary<ConsoleKey, GameAction>()
            {
                {ConsoleKey.UpArrow, GameAction.Up},
                {ConsoleKey.DownArrow, GameAction.Down},
                {ConsoleKey.LeftArrow, GameAction.Left},
                {ConsoleKey.RightArrow, GameAction.Right},
                {ConsoleKey.Escape, GameAction.Quit},
                {ConsoleKey.Z, GameAction.Undo}
            };

            var key = Console.ReadKey().Key;

            GameAction action;
            keyMap.TryGetValue(key, out action);
            return action;
        }

        private static string GetLastCreatedLogFilename()
        {
            return Directory.GetFiles(LogOutputDirectory, "*" + LogExtension)
                            .OrderByDescending(x => x)
                            .FirstOrDefault();

        }
    }
}
