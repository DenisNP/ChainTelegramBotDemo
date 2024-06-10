namespace ChainTelegramBot.Models;

public class State
{
    public long UserId { get; set; }
    public HashSet<int> VisitedPresentations { get; set; } = new();
}
