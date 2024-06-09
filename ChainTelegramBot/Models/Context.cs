using Telegram.Bot.Types;

namespace ChainTelegramBot.Models;

public class Context
{
    public Update Update { get; }
    public State State { get; }

    private int? _firstNumber;
    public int? FirstNumber => _firstNumber ??= 
        Update.Message is { Text.Length: > 0 } 
        && int.TryParse(Update.Message.Text, out int n) 
            ? n 
            : null;

    public Context(Update update, State state)
    {
        Update = update;
        State = state;
    }
}