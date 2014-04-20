using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twos.Models
{
    public class GameState
    {
        private int[,] _board = new int[4,4];
        private readonly LinkedList<GameAction> _actions = new LinkedList<GameAction>(); 
        private readonly Stack<int[,]> _previousBoards = new Stack<int[,]>(); 

        public int Score { get; set; }
        public GameStatus Status { get; set; }
        public LinkedList<GameAction> Actions { get { return _actions; } }
        public Stack<int[,]> PreviousBoards { get { return _previousBoards; } }
        public int[,] Board
        {
            get { return _board; } 
            set { _board = value; }
        }
    }
}
