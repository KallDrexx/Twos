using System;
using Twos.Models;

namespace Twos.Controllers
{
    class MenuDisplayController
    {
        public static GameRunnerParameters DisplayMenu(IMenu menu, GameRunnerParameters parameters = null)
        {
            if (menu == null)
                throw new ArgumentNullException("menu");

            if (parameters == null)
                parameters = new GameRunnerParameters();

            Console.Write(menu.Question);
            Console.WriteLine();
            var answer = Console.ReadLine();

            Console.WriteLine();

            var result = menu.ProcessAnswer(parameters, answer);
            if (result != null)
                DisplayMenu(result, parameters);

            return parameters;
        }
    }
}
