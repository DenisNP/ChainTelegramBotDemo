using ChainTelegramBot.Models;
// ReSharper disable ConvertToPrimaryConstructor
// ReSharper disable PublicConstructorInAbstractClass
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace ChainTelegramBot.Abstract;

public abstract class BaseHandler
{
    protected Context Context { get; }

    public BaseHandler(Context context)
    {
        Context = context;
    }

    public abstract Task<bool> Check();
    public abstract Task Handle();
}