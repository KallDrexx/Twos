﻿using System;
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
2) Exit";
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
                        return null; // No menu to proceed to, so start the game
                    }

                    case 2:
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
