using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twos.Models;
using Twos.Processors;

namespace Twos
{
    class Program
    {
        static void Main()
        {
            var gameParameters = new GameRunnerParameters();

            GameRunner.RunGame(gameParameters);
            Console.ReadLine();
        }
    }
}
