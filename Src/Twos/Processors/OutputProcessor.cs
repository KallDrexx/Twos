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
        private const int ActionsDistanceFromRight = 20;
        private const int ScoreDistanceFromRight = 3;
        private const int SeedDistanceFromRight = 35;

        public OutputProcessor()
        {
            Console.CursorVisible = false;
        }

        public void DisplayGame(GameState state, int seed)
        {
            if (state == null)
                throw new ArgumentNullException("state");

            Console.Clear();
            
            DisplayBoard(state.Board);
            DisplayScore(state.Score);
            DisplayLastActions(state.Actions);
            DisplaySeed(seed);
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
            const string displayLabel = "Score";

            DisplayLabel(displayLabel, ScoreDistanceFromRight);

            int scoreDigits = GetNumberOfDigits(score);
            int startColumn = Console.WindowWidth - ScoreDistanceFromRight - scoreDigits;

            Console.SetCursorPosition(startColumn, 3);
            Console.Write(score);
        }

        private void DisplaySeed(int seed)
        {
            const string displayLabel = "Seed";

            DisplayLabel(displayLabel, SeedDistanceFromRight);

            int scoreDigits = GetNumberOfDigits(seed);
            int startColumn = Console.WindowWidth - SeedDistanceFromRight - scoreDigits;

            Console.SetCursorPosition(startColumn, 3);
            Console.Write(seed);
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
            const int rowStart = 3;

            DisplayLabel(displayLabel, ActionsDistanceFromRight);

            if (actions.Any())
            {
                int row = rowStart;

                var action = actions.Last;
                for (int x = 0; x < displayedActionsCount; x++)
                {
                    if (action == null)
                        break;

                    string text = action.Value.ToString();
                    int startColumn = Console.WindowWidth - text.Length - ActionsDistanceFromRight;
                    Console.SetCursorPosition(startColumn, row);
                    Console.Write(text);

                    action = action.Previous;
                    row++;
                }
            }

        }

        private int GetNumberOfDigits(int value)
        {
            int digits = 1;
            while ((value = value / 10) > 0)
                digits++;

            return digits;
        }

        private static void DisplayLabel(string displayLabel, int distanceFromRight)
        {
            int row = 1;
            int startColumn = Console.WindowWidth - displayLabel.Length - distanceFromRight;
            Console.SetCursorPosition(startColumn, row);
            Console.Write(displayLabel);
            row++;

            Console.SetCursorPosition(startColumn, row);
            for (int x = 0; x < displayLabel.Length; x++)
                Console.Write("-");
        }
    }
}
