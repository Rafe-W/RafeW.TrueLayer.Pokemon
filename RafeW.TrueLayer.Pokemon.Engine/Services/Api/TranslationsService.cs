using RafeW.TrueLayer.Pokemon.Engine.Helpers;
using RafeW.TrueLayer.Pokemon.Engine.Services.Utilities;

namespace RafeW.TrueLayer.Pokemon.Engine.Services.Api
{
    public interface ITranslationsService
    {

    }

    [Injectable]
    public class TranslationsService
    {
        private ICacheService CacheService { get; }

        public TranslationsService(ICacheService cacheService)
        {
            CacheService = cacheService;
        }
    }
}
