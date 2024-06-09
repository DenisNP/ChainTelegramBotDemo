using Telegram.Bot.Types;

namespace ChainTelegramBot.Abstract;

public interface IChainService
{
    public Task HandleUpdateAsync(Update update);
}