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
    public static class GameRunner
    {
        public static void RunGame(GameRunnerParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            var output = new OutputProcessor();
            var actionProcessor = new GameActionProcessor(parameters.GameSeed);

            var state = actionProcessor.GenerateInitialBoard();
            output.DisplayGame(state, actionProcessor.Seed);

            var logFileName = Path.Combine(parameters.LogOutputDirectory, DateTime.Now.ToString("yyyyMMddhhmmss") + parameters.LogOutputExtension);
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
    }
}
