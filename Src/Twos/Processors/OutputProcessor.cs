using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twos.Models;

namespace Twos.Processors
{
    public class OutputProcessor
    {
        private const int TileDisplayWidth = 7;

        public void DisplayGame(GameState state)
        {
            if (state == null)
                throw new ArgumentNullException("state");

            Console.Clear();
            
            DisplayBoard(state.Board);
            DisplayScore(state.Score);
            DisplayLastActions(state.Actions);
        }

        private void DisplayBoard(int[,] board)
        {
            const int distanceFromTop = 1;
            const int distanceFromLeft = 2;

            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int column = 0; column < board.GetLength(1); column++)
                {
                    int positionX = (row * TileDisplayWidth) + distanceFromLeft;
                    int positionY = column + distanceFromTop;

                    DisplayTileValue(positionX, positionY, board[row, column]);
                }
            }
        }

        private void DisplayScore(int score)
        {
            const string displayLabel = "Score:";
            const int distanceFromBorder = 3;

            int row = 1;
            int startColumn = Console.WindowWidth - displayLabel.Length - distanceFromBorder;
            Console.SetCursorPosition(startColumn, row);
            Console.Write(displayLabel);
            row++;

            Console.SetCursorPosition(startColumn, row);
            for (int x = 0; x < displayLabel.Length; x++)
                Console.Write("-");

            row++;

            int scoreDigits = GetNumberOfDigits(score);
            startColumn = Console.WindowWidth - distanceFromBorder - scoreDigits;

            Console.SetCursorPosition(startColumn, row);
            Console.Write(score);
        }

        private void DisplayTileValue(int startX, int startY, int value)
        {
            int digitCount = GetNumberOfDigits(value);
            startX += (TileDisplayWidth - digitCount);

            Console.SetCursorPosition(startX, startY);
            Console.Write(value);
        }

        private void DisplayLastActions(LinkedList<GameAction> actions)
        {
            const string displayLabel = "Actions";
            const int displayedActionsCount = 5;
            const int distanceFromBorder = 20;
            const int rowStart = 1;

            int row = rowStart;
            int startColumn = Console.WindowWidth - displayLabel.Length - distanceFromBorder;
            Console.SetCursorPosition(startColumn, row);
            Console.Write(displayLabel);
            row++;

            Console.SetCursorPosition(startColumn, row);
            for (int x = 0; x < displayLabel.Length; x++)
                Console.Write("-");

            row++;

            if (actions.Any())
            {
                var action = actions.Last;
                for (int x = 0; x < displayedActionsCount; x++)
                {
                    if (action == null)
                        break;

                    string text = action.Value.ToString();
                    startColumn = Console.WindowWidth - text.Length - distanceFromBorder;
                    Console.SetCursorPosition(startColumn, row);
                    Console.Write(text);

                    action = action.Previous;
                    row++;
                }
            }

        }

        private int GetNumberOfDigits(int value)
        {
            int digits = 0;
            while ((value = value % 10) > 0)
                digits++;

            if (digits == 0)
                digits = 1;

            return digits;
        }
    }
}
