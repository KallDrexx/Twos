using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twos
{
    public static class MatrixHelper
    {
        public static void Rotate90Degrees(int[,] matrix)
        {
            Transpose(matrix);
            ReverseRows(matrix);
        }

        public static void RotateNegative90Degrees(int[,] matrix)
        {
            Transpose(matrix);
            ReverseColumns(matrix);
        }

        public static void Rotate180Degrees(int[,] matrix)
        {
            ReverseRows(matrix);
            ReverseColumns(matrix);
        }

        private static void Transpose(int[,] matrix)
        {
            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = x; y < matrix.GetLength(1); y++)
                {
                    int tempValue = matrix[x, y];
                    matrix[x, y] = matrix[y, x];
                    matrix[y, x] = tempValue;
                }
            }
        }

        private static void ReverseRows(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1) / 2; col++)
                {
                    int length = matrix.GetLength(1);
                    int temp = matrix[row, col];
                    matrix[row, col] = matrix[row, length - col - 1];
                    matrix[row, length - col - 1] = temp;
                }
            }
        }

        public static void ReverseColumns(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0) / 2; row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    int length = matrix.GetLength(0);
                    int temp = matrix[row, col];
                    matrix[row, col] = matrix[length - row - 1, col];
                    matrix[length - row - 1, col] = temp;
                }
            }
        }
    }
}
