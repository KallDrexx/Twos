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
        private const int TileDisplayWidth = 5;

        public void DisplayGame(GameState state)
        {
            if (state == null)
                throw new ArgumentNullException("state");

            Console.Clear();
            
            DisplayBoard(state.Board);
        }

        private void DisplayBoard(int[,] board)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int column = 0; column < board.GetLength(1); column++)
                {
                    int positionX = (row * TileDisplayWidth);
                    int positionY = column;

                    DisplayTileValue(positionX, positionY, board[row, column]);
                }
            }
        }

        private void DisplayTileValue(int startX, int startY, int value)
        {
            int digitCount = GetNumberOfDigits(value);
            startX += (TileDisplayWidth - digitCount);

            Console.SetCursorPosition(startX, startY);
            Console.Write(value);
        }

        private int GetNumberOfDigits(int value)
        {
            int digits = 0;
            while ((value = value % 10) > 0)
                digits++;

            return digits;
        }
    }
}
