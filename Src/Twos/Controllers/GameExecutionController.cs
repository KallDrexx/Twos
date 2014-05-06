using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Twos.Models;
using Twos.Processors;

namespace Twos.Controllers
{
    static class GameExecutionController
    {
        private const decimal SecondsBetweenComputerControlledActions = 0.5m;

        public static void RunGame(GameRunnerParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            var output = new OutputProcessor();
            var actionProcessor = new GameActionProcessor(parameters.GameSeed);

            var state = actionProcessor.GenerateInitialBoard();
            output.DisplayGame(state, actionProcessor.Seed);

            bool isReplayingLoggedGame = parameters.ReplayActions.Any();
            int replayActionIndex = 0;

            var writer = isReplayingLoggedGame
                             ? null
                             : new ActionLogWriter(GetNewLogName(), actionProcessor.Seed);

            while (state.Status == GameStatus.InProgress)
            {
                GameAction action;

                if (isReplayingLoggedGame)
                {
                    // Replaying logged game
                    if (replayActionIndex >= parameters.ReplayActions.Length)
                    {
                        Console.WriteLine("Logged game ended without any end condition being met!");
                        break;
                    }

                    action = parameters.ReplayActions[replayActionIndex];
                    replayActionIndex++;

                    Thread.Sleep((int)(SecondsBetweenComputerControlledActions * 1000));
                }
                else
                {
                    // Player is playing the game
                    action = GetActionFromKeyPress();
                }

                actionProcessor.RunGameAction(state, action);

                output.DisplayGame(state, actionProcessor.Seed);

                if (writer != null)
                {
                    writer.LogAction(action);
                }
            }

            if (writer != null)
            {
                writer.Dispose();
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

        private static string GetNewLogName()
        {
            return Path.Combine(GameSettings.LogOutputDirectory, DateTime.Now.ToString("yyyyMMddhhmmss") + GameSettings.LogExtension);
        }
    }
}
