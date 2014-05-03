namespace Twos.Models
{
    public class LoggedGame
    {
        public int GameSeed { get; set; }
        public GameAction[] Actions { get; set; }
    }
}
