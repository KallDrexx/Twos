namespace Twos.Models
{
    interface IMenu
    {
        string Question { get; }
        IMenu ProcessAnswer(GameRunnerParameters gameRunnerParameters, string answer);
    }
}
