using ChainTelegramBot.Abstract;
using ChainTelegramBot.Models;
using ChainTelegramBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ChainTelegramBot.Handlers;

public class GetLanguageHandler : BaseHandler
{
    private readonly IStateStorage _stateStorage;
    private readonly ITelegramBotClient _client;

    public GetLanguageHandler(Context context, TelegramService telegramService, IStateStorage stateStorage) : base(context)
    {
        _stateStorage = stateStorage;
        _client = telegramService.Client;
    }

    public override bool Check()
    {
        return string.IsNullOrEmpty(Context.State.Language) && Context.Update is { Message.Text.Length: >0 };
    }

    public override async Task Handle()
    {
        string lang = Context.Update.Message!.Text!;
        Context.State.Language = lang;

        await _stateStorage.SaveState(Context.State);

        await _client.SendTextMessageAsync(
            new ChatId(Context.State.UserId),
            "Отлично. А какой опыт в годах?"
        );
    }
}