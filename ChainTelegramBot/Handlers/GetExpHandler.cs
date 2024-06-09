using ChainTelegramBot.Abstract;
using ChainTelegramBot.Models;
using ChainTelegramBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ChainTelegramBot.Handlers;

public class GetExpHandler : BaseHandler
{
    private readonly ITelegramBotClient _client;
    private readonly IStateStorage _stateStorage;
    private readonly JugRecommendationService _recommendationService;

    public GetExpHandler(Context context, TelegramService telegramService, IStateStorage stateStorage,
        JugRecommendationService recommendationService) : base(context)
    {
        _client = telegramService.Client;
        _stateStorage = stateStorage;
        _recommendationService = recommendationService;
    }

    public override bool Check()
    {
        return !Context.State.Experience.HasValue && Context.FirstNumber.HasValue;
    }

    public override async Task Handle()
    {
        Context.State.Experience = Context.FirstNumber!.Value;
        await _stateStorage.SaveState(Context.State);

        string recom = _recommendationService.GetRecommendation(
            Context.State.Language,
            Context.State.Experience.Value
        );

        await _client.SendTextMessageAsync(
            new ChatId(Context.State.UserId),
            $"Рекомендую вам доклад:\n{recom}"
        );
    }
}