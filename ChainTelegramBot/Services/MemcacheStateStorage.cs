using ChainTelegramBot.Abstract;
using ChainTelegramBot.Models;
using Microsoft.Extensions.Caching.Memory;

namespace ChainTelegramBot.Services;

public class MemcacheStateStorage(IMemoryCache memoryCache) : IStateStorage
{
    public Task<State> GetStateFor(long userId)
    {
        memoryCache.TryGetValue(userId, out State? state);
        return Task.FromResult(state ?? new State { UserId = userId });
    }

    public Task SaveState(State state)
    {
        memoryCache.Set(state.UserId, state, TimeSpan.FromDays(1));
        return Task.CompletedTask;
    }
}