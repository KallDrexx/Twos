using System;
using System.IO;
using Twos.Models;
using Twos.Processors;

namespace Twos.Menus
{
    class ReplayLoggedGameMenu : IMenu
    {
        public string Question
        {
            get { return @"Enter the path to the log to replay (or leave blank to go back)"; }
        }

        public IMenu ProcessAnswer(GameRunnerParameters gameRunnerParameters, string answer)
        {
            if (string.IsNullOrWhiteSpace(answer))
            {
                return new MainMenu();
            }

            try
            {
                var loggedGame = ActionLogReader.ReadLog(answer);
                gameRunnerParameters.GameSeed = loggedGame.GameSeed;
                gameRunnerParameters.ReplayActions = loggedGame.Actions;

                return null;
            }
            catch (Exception ex)
            {
                if (ex is IOException || ex is UnauthorizedAccessException)
                {
                    Console.WriteLine("The selected file could not be read");
                    return this;
                }

                throw;
            }
        }
    }
}
