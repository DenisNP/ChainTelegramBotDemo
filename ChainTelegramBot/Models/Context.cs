using ChainTelegramBot.Abstract;
using Telegram.Bot.Types;

namespace ChainTelegramBot.Models;

public class Context
{
    public Update Update { get; }
    public State State { get; }

    public bool ReceivedNext => Update.Message?.Text?.StartsWith("/next") == true;

    private Presentation? _firstUnvisited;
    public async Task<Presentation?> GetFirstUnvisited(IPresentationStorage presentationStorage)
    {
        if (_firstUnvisited == null)
        {
            var presentations = await presentationStorage.GetAvailablePresentations();
            _firstUnvisited = presentations.FirstOrDefault(
                p => p.Time >= DateTime.Now && !State.VisitedPresentations.Contains(p.Id)
            );
        }

        return _firstUnvisited;
    }

    public Context(Update update, State state)
    {
        Update = update;
        State = state;
    }
}