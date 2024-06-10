using ChainTelegramBot.Abstract;
using ChainTelegramBot.Models;

namespace ChainTelegramBot.Services;

public class StaticPresentationStorage : IPresentationStorage
{
    public Task<List<Presentation>> GetAvailablePresentations()
    {
        return Task.FromResult(
            new List<Presentation>{
                new(1, "Continuous disintegration", DateTime.Now - TimeSpan.FromHours(1)),
                new(2, "Паттерны кафельной плитки", DateTime.Now + TimeSpan.FromHours(1)),
                new(3, "Многопоточный Hello, world", DateTime.Now + TimeSpan.FromHours(2))
            }
        );
    }
}