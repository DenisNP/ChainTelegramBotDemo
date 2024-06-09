using ChainTelegramBot.Abstract;
using ChainTelegramBot.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<TelegramService>();
builder.Services.AddSingleton<JugRecommendationService>();
builder.Services.AddScoped<IStateStorage, MemcacheStateStorage>();
builder.Services.AddScoped<IChainService, ChainService>();

WebApplication app = builder.Build();

app.Services.GetRequiredService<TelegramService>().Run();
app.Run();
