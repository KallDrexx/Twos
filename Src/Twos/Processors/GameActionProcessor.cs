using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        
    }
}
