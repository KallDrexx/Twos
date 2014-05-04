using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twos.Models;

namespace Twos.Menus
{
    class MainMenu : IMenu
    {
        public string Question 
        {
            get
            {
                return @"Welcome to Twos!

What would you like to do?
1) Play game
2) Replay logged game
3) Exit";
            }
        }

        public IMenu ProcessAnswer(GameRunnerParameters gameRunnerParameters, string answer)
        {
            int value;

            if (int.TryParse(answer, out value))
            {
                switch (value)
                {
                    case 1:
                    {
                        return new PlayMenu();
                    }

                    case 2:
                    {
                        return new ReplayLoggedGameMenu();
                    }

                    case 3:
                    {
                        gameRunnerParameters.QuitWithoutPlaying = true;
                        return null;
                    }
                }
            }

            Console.WriteLine("Invalid option: {0}", answer);
            return this;
        }
    }
}
