using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twos.Models;

namespace Twos.Processors
{
    public class GameActionProcessor
    {
        private readonly Random _random;

        public int Seed { get; private set; }

        public GameActionProcessor(int? seed = null)
        {
            if (seed == null)
                seed = new Random().Next();

            _random = new Random(seed.Value);
            Seed = seed.Value;
        }

        public GameState GenerateInitialBoard()
        {
            var state = new GameState
            {
                Status = GameStatus.InProgress
            };

            AddTileToBoard(state);
            AddTileToBoard(state);

            return state;
        }

        public void RunGameAction(GameState state, GameAction action)
        {
            bool isQuitting = false;
            state.Actions.AddLast(action);
            bool atLeastOneTileMoved = false;

            switch (action)
            {
                case GameAction.Quit:
                {
                    state.Status = GameStatus.Quit;
                    isQuitting = true;
                    break;
                }

                // To simplify the calculations, rotate the board so we are always
                // sliding to the left, then rotate it back
                case GameAction.Left:
                {
                    atLeastOneTileMoved = SlideTilesToLeft(state);
                    break;
                }

                case GameAction.Up:
                {
                    MatrixHelper.RotateNegative90Degrees(state.Board);
                    atLeastOneTileMoved = SlideTilesToLeft(state);
                    MatrixHelper.Rotate90Degrees(state.Board);
                    break;
                }

                case GameAction.Right:
                {
                    MatrixHelper.Rotate180Degrees(state.Board);
                    atLeastOneTileMoved = SlideTilesToLeft(state);
                    MatrixHelper.Rotate180Degrees(state.Board);
                    break;
                }

                case GameAction.Down:
                {
                    MatrixHelper.Rotate90Degrees(state.Board);
                    atLeastOneTileMoved = SlideTilesToLeft(state);
                    MatrixHelper.RotateNegative90Degrees(state.Board);
                    break;
                }
            }

            if (!isQuitting)
            {
                if (atLeastOneTileMoved)
                    AddTileToBoard(state);

                if (!AnyMovesPossible(state.Board))
                    state.Status = GameStatus.Lost;
            }
        }
        
        private void AddTileToBoard(GameState state)
        {
            // Create a list of all empty tiles and pick one from random
            var emptyCoordinates = new List<Coordinate>();

            for (int row = 0; row < state.Board.GetLength(0); row++)
            {
                for (int col = 0; col < state.Board.GetLength(1); col++)
                {
                    if (state.Board[row, col] == 0)
                        emptyCoordinates.Add(new Coordinate(row, col));
                }
            }

            if (emptyCoordinates.Any())
            {
                int indexToAdd = _random.Next(0, emptyCoordinates.Count);
                var coords = emptyCoordinates[indexToAdd];
                state.Board[coords.Row, coords.Column] = 2;
            }
        }

        /// <summary>
        /// Slides all tiles to the left, performing any merges along the way
        /// </summary>
        /// <param name="state"></param>
        /// <returns>false if no tiles were moved or merged, true if a move or merge occurred</returns>
        private bool SlideTilesToLeft(GameState state)
        {
            var board = state.Board;
            bool atLeastOneTileMoved = false;

            for (int row = 0; row < board.GetLength(0); row++)
            {
                int emptyColumn = -1;
                int lastNonEmptyColumn = -1;

                for (int col = 0; col < board.GetLength(1); col++)
                {
                    int tileValue = board[row, col];

                    // Check if the tile is empty
                    if (tileValue == 0)
                    {
                        // If this is the first empty tile mark it
                        if (emptyColumn == -1)
                            emptyColumn = col;
                    }
                    else
                    {
                        // If there is a non-empty column to the left, 
                        // check if they can be merged
                        if (lastNonEmptyColumn >= 0 && board[row, lastNonEmptyColumn] == tileValue)
                        {
                            // Merge
                            board[row, lastNonEmptyColumn] = (tileValue*2);
                            board[row, col] = 0; // current tile is now empty
                            state.Score += (tileValue*2);
                            atLeastOneTileMoved = true;
                        }
                        else if (emptyColumn >= 0)
                        {
                            // Slide
                            board[row, emptyColumn] = tileValue;
                            lastNonEmptyColumn = emptyColumn;
                            board[row, col] = 0;
                            emptyColumn = col;
                            atLeastOneTileMoved = true;
                        }
                        else
                        {
                            lastNonEmptyColumn = col;
                        }
                    }
                }
            }

            return atLeastOneTileMoved;
        }

        private bool AnyMovesPossible(int[,] board)
        {
            // A move is possible if there are any empty spaces
            // or if there are any of the same values next to each other
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    int tileValue = board[row, col];

                    if (tileValue == 0)
                        return true;

                    if (row > 0 && board[row - 1, col] == tileValue)
                        return true;

                    if (row < board.GetLength(0) - 1 && board[row + 1, col] == tileValue)
                        return true;

                    if (col > 0 && board[row, col - 1] == tileValue)
                        return true;

                    if (col < board.GetLength(1) - 1 && board[row, col + 1] == tileValue)
                        return true;
                }
            }

            // No moves found
            return false;
        }
    }
}
