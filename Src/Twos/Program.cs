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
        private const string LogOutputDirectory = @"C:\temp";
        private const string LogExtension = ".twos";

        static void Main()
        {
            var gameParameters = new GameRunnerParameters
            {
                LogOutputDirectory = LogOutputDirectory,
                LogOutputExtension = LogExtension
            };

            GameRunner.RunGame(gameParameters);

            Console.ReadLine();
        }
    }
}
