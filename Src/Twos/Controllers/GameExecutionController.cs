using System;
using System.Collections.Generic;
using System.IO;
using Twos.Models;
using Twos.Processors;

namespace Twos.Controllers
{
    class GameExecutionController
    {
        public static void RunGame(GameRunnerParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            var output = new OutputProcessor();
            var actionProcessor = new GameActionProcessor(parameters.GameSeed);

            var state = actionProcessor.GenerateInitialBoard();
            output.DisplayGame(state, actionProcessor.Seed);

            var logFileName = Path.Combine(GameSettings.LogOutputDirectory, DateTime.Now.ToString("yyyyMMddhhmmss") + GameSettings.LogExtension);
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
