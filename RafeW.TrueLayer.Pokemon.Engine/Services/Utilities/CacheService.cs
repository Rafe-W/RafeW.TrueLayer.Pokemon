using Microsoft.Extensions.Caching.Memory;
using RafeW.TrueLayer.Pokemon.Engine.Helpers;
using System;

namespace RafeW.TrueLayer.Pokemon.Engine.Services.Utilities
{
    public interface ICacheService
    {
        T ProcessCaching<T>(string cacheKey, Func<T> retrieveCacheableData, TimeSpan expirationTimeSpan);
    }

    [Injectable]
    public class CacheService : ICacheService
    {
        private IMemoryCache MemoryCache { get; }

        public CacheService(IMemoryCache memoryCache)
        {
            MemoryCache = memoryCache;
        }

        public T ProcessCaching<T>(string cacheKey, Func<T> retrieveCacheableData, TimeSpan expirationTimeSpan)
        {
            if (MemoryCache.TryGetValue(cacheKey, out T cachedValue))
                return cachedValue;

            var valueToCache = retrieveCacheableData.Invoke();
            if (valueToCache == null)
                return default;

            using (var entry = MemoryCache.CreateEntry(cacheKey))
            {
                entry.Value = valueToCache;
                entry.SetAbsoluteExpiration(expirationTimeSpan);
            }

            return valueToCache;
        }
    }
}
