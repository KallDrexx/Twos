using Twos.Models;

namespace Twos.Menus
{
    class PlayMenu : IMenu
    {
        public string Question
        {
            get { return @"Enter numeric seed for game (or leave blank for random seed):"; }
        }

        public IMenu ProcessAnswer(GameRunnerParameters gameRunnerParameters, string answer)
        {
            int seed;
            if (int.TryParse(answer, out seed))
            {
                gameRunnerParameters.GameSeed = seed;
            }

            return null;
        }
    }
}
