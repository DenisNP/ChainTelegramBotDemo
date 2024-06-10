using ChainTelegramBot.Abstract;
using ChainTelegramBot.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<TelegramService>();

builder.Services.AddScoped<IStateStorage, MemcacheStateStorage>();
builder.Services.AddScoped<IPresentationStorage, StaticPresentationStorage>();
builder.Services.AddScoped<IChainService, ChainService>();

WebApplication app = builder.Build();

app.Services.GetRequiredService<TelegramService>().Run();
app.Run();
