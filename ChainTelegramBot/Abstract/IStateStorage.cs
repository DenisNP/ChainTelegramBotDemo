using ChainTelegramBot.Models;

namespace ChainTelegramBot.Abstract;

public interface IStateStorage
{
    public Task<State> GetStateFor(long userId);
    public Task SaveState(State state);
}