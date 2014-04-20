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
            var state = new GameState();
            var output = new OutputProcessor();
            LinkedListNode<GameAction> lastAction = null;

            output.DisplayGame(state, 123456);
            while (state.Status == GameStatus.InProgress)
            {
                var action = GetActionFromKeyPress();
                if (action != GameAction.None)
                {
                    if (lastAction == null)
                        lastAction = state.Actions.AddFirst(action);
                    else
                        lastAction = state.Actions.AddAfter(lastAction, action);
                }

                output.DisplayGame(state, 123456);
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
