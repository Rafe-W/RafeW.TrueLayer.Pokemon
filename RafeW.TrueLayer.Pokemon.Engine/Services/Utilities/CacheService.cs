using Microsoft.Extensions.Caching.Memory;
using RafeW.TrueLayer.Pokemon.Engine.Helpers;

namespace RafeW.TrueLayer.Pokemon.Engine.Services.Utilities
{
    public interface ICacheService
    {

    }

    [Injectable]
    public class CacheService
    {
        private IMemoryCache MemoryCache { get; }

        public CacheService(IMemoryCache memoryCache)
        {
            MemoryCache = memoryCache;
        }
    }
}
