using ChainTelegramBot.Abstract;
using ChainTelegramBot.Models;
using ChainTelegramBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ChainTelegramBot.Handlers;

public class EnterHandler : BaseHandler
{
    private readonly ITelegramBotClient _client;

    public EnterHandler(Context context, TelegramService telegramService) : base(context)
    {
        _client = telegramService.Client;
    }

    public override bool Check()
    {
        return Context.Update.Message?.Text?.StartsWith("/start") == true;
    }

    public override async Task Handle()
    {
        await _client.SendTextMessageAsync(
            new ChatId(Context.State.UserId),
            "Приветствую вас. Какой у вас любимый язык программирования?"
        );
    }
}