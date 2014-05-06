using System.Collections.Generic;

namespace Twos.Models
{
    public class MoveStatistics
    {
        public bool MoveOccurred { get; set; }
        public List<int> MergeResults { get; private set; } 

        public MoveStatistics()
        {
            MergeResults = new List<int>();
        }
    }
}
