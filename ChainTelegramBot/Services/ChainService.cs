using ChainTelegramBot.Abstract;
using ChainTelegramBot.Handlers;
using ChainTelegramBot.Models;
using Telegram.Bot.Types;

namespace ChainTelegramBot.Services;

public class ChainService(IServiceProvider serviceProvider, IStateStorage stateStorage) : IChainService
{
    private readonly Type[] _chain = [
        typeof(ShowMenuHandler),
        typeof(ToggleVisitedHandler),
        typeof(GetNextPresentation),
        typeof(NoNextPresentation),
    ];

    private async Task Handle(Context ctx)
    {
        foreach (Type handlerType in _chain)
        {
            var handler = (BaseHandler)ActivatorUtilities.CreateInstance(serviceProvider, handlerType, ctx)!;
            if (await handler.Check())
            {
                await handler.Handle();
                break;
            }
        }
    }

    public async Task HandleUpdateAsync(Update update)
    {
        long? userId = update.GetUserId();
        if (!userId.HasValue)
        {
            return;
        }

        State state = await stateStorage.GetStateFor(userId.Value);

        var ctx = new Context(update, state);
        await Handle(ctx);
    }
}