using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twos.Models;
using Twos.Processors;

namespace Twos
{
    class Program
    {
        static void Main(string[] args)
        {
            var state = new GameState();
            var output = new OutputProcessor();

            output.DisplayGame(state);

            Console.ReadLine();
        }
    }
}
