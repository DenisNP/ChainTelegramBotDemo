using ChainTelegramBot.Abstract;
using ChainTelegramBot.Models;
using ChainTelegramBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ChainTelegramBot.Handlers;

public class ShowMenuHandler : BaseHandler
{
    private readonly IPresentationStorage _presentationStorage;
    private readonly ITelegramBotClient _client;

    public ShowMenuHandler(
        Context context,
        TelegramService telegramService,
        IPresentationStorage presentationStorage
    ) : base(context)
    {
        _presentationStorage = presentationStorage;
        _client = telegramService.Client;
    }

    public override Task<bool> Check()
    {
        return Task.FromResult(Context.Update.Message?.Text?.StartsWith("/start") == true);
    }

    public override async Task Handle()
    {
        await _client.SendTextMessageAsync(
            new ChatId(Context.State.UserId),
            "Доступные доклады:",
            replyMarkup: await GetReplyMarkup(_presentationStorage, Context)
        );
    }

    public static async Task<InlineKeyboardMarkup> GetReplyMarkup(IPresentationStorage presentationStorage, Context context)
    {
        List<Presentation> presentations = await presentationStorage.GetAvailablePresentations();

        return new InlineKeyboardMarkup(
            presentations.Select(p => new[] { p.AsButton(context.State.VisitedPresentations.Contains(p.Id)) })
        );
    }
}