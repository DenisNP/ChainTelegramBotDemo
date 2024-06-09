using ChainTelegramBot.Abstract;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace ChainTelegramBot.Services;

public class TelegramService : IUpdateHandler
{
    public ITelegramBotClient Client => _client ?? throw new Exception($"Start {nameof(TelegramService)} first");

    private readonly TelegramBotClient _client;
    private readonly IServiceProvider _serviceProvider;

    public TelegramService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        string apiToken = Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN")
                          ?? throw new Exception("No TELEGRAM_BOT_TOKEN env variable");

        _client = new TelegramBotClient(apiToken);
    }

    public void Run()
    {
        _client.StartReceiving(this);
        Console.WriteLine(nameof(TelegramService) + " is running");
    }

    public Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateAsyncScope();
        var chainService = scope.ServiceProvider.GetRequiredService<IChainService>();
        return chainService.HandleUpdateAsync(update);
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(exception);
        return Task.CompletedTask;
    }
}