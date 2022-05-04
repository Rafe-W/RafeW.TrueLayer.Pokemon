using RafeW.TrueLayer.Pokemon.Engine.Helpers;
using RafeW.TrueLayer.Pokemon.Engine.Services.Utilities;

namespace RafeW.TrueLayer.Pokemon.Engine.Services.Api
{
    public interface IPokeAPIService
    {

    }

    [Injectable]
    public class PokeAPIService
    {
        private ICacheService CacheService { get; }

        public PokeAPIService(ICacheService cacheService)
        {
            CacheService = cacheService;
        }
    }
}
