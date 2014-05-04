using System;
using Twos.Controllers;
using Twos.Menus;

namespace Twos
{
    class Program
    {
        static void Main()
        {
            var parameters = MenuDisplayController.DisplayMenu(new MainMenu());
            if (!parameters.QuitWithoutPlaying)
            {
                GameExecutionController.RunGame(parameters);
            }

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
