using ChainTelegramBot.Models;

namespace ChainTelegramBot.Abstract;

public interface IPresentationStorage
{
    public Task<List<Presentation>> GetAvailablePresentations();
}