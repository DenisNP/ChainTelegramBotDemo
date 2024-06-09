using Telegram.Bot.Types;

namespace ChainTelegramBot.Models;

public class Context
{
    public Update Update { get; }
    public State State { get; }

    public Context(Update update, State state)
    {
        Update = update;
        State = state;
    }
}