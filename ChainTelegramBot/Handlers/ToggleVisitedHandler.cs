using ChainTelegramBot.Abstract;
using ChainTelegramBot.Models;
using ChainTelegramBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ChainTelegramBot.Handlers;

public class ToggleVisitedHandler : BaseHandler
{
    private readonly ITelegramBotClient _client;
    private readonly IPresentationStorage _presentationStorage;
    private readonly IStateStorage _stateStorage;

    public ToggleVisitedHandler(
        Context context,
        TelegramService telegramService,
        IPresentationStorage presentationStorage,
        IStateStorage stateStorage
    ) : base(context)
    {
        _client = telegramService.Client;
        _presentationStorage = presentationStorage;
        _stateStorage = stateStorage;
    }

    public override Task<bool> Check()
    {
        return Task.FromResult(Context.Update.CallbackQuery?.Data?.StartsWith("toggle_visited_") == true);
    }

    public override async Task Handle()
    {
        // modify state
        var pId = int.Parse(Context.Update.CallbackQuery!.Data!.Split("_")[^1]);
        if (!Context.State.VisitedPresentations.Add(pId))
        {
            Context.State.VisitedPresentations.Remove(pId);
        }

        await _stateStorage.SaveState(Context.State);

        // respond
        await _client.AnswerCallbackQueryAsync(Context.Update.CallbackQuery.Id);
        await _client.EditMessageReplyMarkupAsync(
            new ChatId(Context.State.UserId),
            Context.Update.CallbackQuery.Message!.MessageId,
            replyMarkup: await ShowMenuHandler.GetReplyMarkup(_presentationStorage, Context)
        );
    }
}