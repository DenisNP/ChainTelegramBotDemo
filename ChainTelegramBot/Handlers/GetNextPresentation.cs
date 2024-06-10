using ChainTelegramBot.Abstract;
using ChainTelegramBot.Models;
using ChainTelegramBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ChainTelegramBot.Handlers;

public class GetNextPresentation : BaseHandler
{
    private readonly ITelegramBotClient _client;
    private readonly IPresentationStorage _presentationStorage;

    private Presentation? _firstUnvisited;
    
    public GetNextPresentation(
        Context context,
        TelegramService telegramService,
        IPresentationStorage presentationStorage
    ) : base(context)
    {
        _client = telegramService.Client;
        _presentationStorage = presentationStorage;
    }

    public override async Task<bool> Check()
    {
        if (!Context.ReceivedNext)
        {
            return false;
        }

        _firstUnvisited = await Context.GetFirstUnvisited(_presentationStorage);
        return _firstUnvisited != null;
    }

    public override async Task Handle()
    {
        await _client.SendTextMessageAsync(
            new ChatId(Context.State.UserId),
            "Следующий доступный доклад:\n\n" + _firstUnvisited!.NameWithTime()
        );
    }
}