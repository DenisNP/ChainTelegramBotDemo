using ChainTelegramBot.Abstract;
using ChainTelegramBot.Models;
using Telegram.Bot.Types;
// ReSharper disable InvertIf
// ReSharper disable UnusedMember.Local

namespace ChainTelegramBot.Services;

public class ChainService(IServiceProvider serviceProvider, IStateStorage stateStorage) : IChainService
{
    private readonly Type[] _chain = [
        // typeof(SomeHandler1),
        // typeof(SomeHandler2)
        // ...
    ];

    private async Task Handle(Context ctx)
    {
        foreach (Type handlerType in _chain)
        {
            var handler = (BaseHandler)ActivatorUtilities.CreateInstance(serviceProvider, handlerType, ctx)!;
            if (handler.Check())
            {
                await handler.Handle();
                break;
            }
        }
    }

    public async Task HandleUpdateAsync(Update update)
    {
        if (update is not { Message.From: not null })
        {
            return;
        }

        long userId = update.Message.From.Id;
        State state = await stateStorage.GetStateFor(userId);

        var ctx = new Context(update, state);
        await Handle(ctx);
    }
}