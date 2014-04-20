using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twos.Models;
using Twos.Processors;

namespace Twos
{
    class Program
    {
        static void Main(string[] args)
        {
            var output = new OutputProcessor();
            var actionProcessor = new GameActionProcessor();
            LinkedListNode<GameAction> lastAction = null;

            var state = actionProcessor.GenerateInitialBoard();
            output.DisplayGame(state, actionProcessor.Seed);

            while (state.Status == GameStatus.InProgress)
            {
                var action = GetActionFromKeyPress();
                if (action != GameAction.None)
                {
                    lastAction = lastAction == null 
                        ? state.Actions.AddFirst(action) 
                        : state.Actions.AddAfter(lastAction, action);
                }

                output.DisplayGame(state, actionProcessor.Seed);
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
                {ConsoleKey.Escape, GameAction.Quit}
            };

            var key = Console.ReadKey().Key;

            GameAction action;
            keyMap.TryGetValue(key, out action);
            return action;
        }
    }
}
