using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twos.Models
{
    public class GameState
    {
        private readonly int[,] _board = new int[4,4];
        private readonly LinkedList<GameAction> _actions = new LinkedList<GameAction>(); 

        public int Score { get; set; }
        public LinkedList<GameAction> Actions { get { return _actions; } }
        public int[,] Board { get { return _board; } }
    }
}
