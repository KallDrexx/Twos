namespace Twos.Models
{
    public class GameRunnerParameters
    {
        public int? GameSeed { get; set; }
        public GameAction[] ReplayActions { get; set; }
        public bool QuitWithoutPlaying { get; set; }

        public GameRunnerParameters()
        {
            ReplayActions = new GameAction[0];
        }
    }
}
