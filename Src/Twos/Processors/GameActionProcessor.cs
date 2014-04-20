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
            state.Actions.AddLast(action);

            switch (action)
            {
                case GameAction.Quit:
                {
                    state.Status = GameStatus.Quit;
                    break;
                }
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
    }
}
