using Microsoft.Extensions.Caching.Memory;
using Moq;
using RafeW.TrueLayer.Pokemon.Engine.Entities.Utilities;
using RafeW.TrueLayer.Pokemon.Engine.Services.Api;
using RafeW.TrueLayer.Pokemon.Engine.Services.Utilities;
using RafeW.TrueLayer.Pokemon.Engine.Tests.TestHelpers.ServiceContainers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafeW.TrueLayer.Pokemon.Engine.Tests.TestHelpers.ServiceContainers.Api
{
    public class TranslationsServiceContainer
    {
        public Mock<ICacheService> CacheService { get; set; }
        public Mock<IRequestHandlerService<Translations_ApiSettings>> RequestHandlerService { get; set; }
        public TranslationsService TranslationsService { get; set; }

        public TranslationsServiceContainer(bool useRealCacheService)
        {
            //Construct Mocks
            CacheService = useRealCacheService ? null : new Mock<ICacheService>();
            RequestHandlerService = new Mock<IRequestHandlerService<Translations_ApiSettings>>();

            CacheServiceContainer cacheContainer = null;

            if (useRealCacheService) {
                //Need to mock cache entry to stop null errors. Other methods can return defaults without issue
                cacheContainer = new CacheServiceContainer();
                var cacheEntry = new Mock<ICacheEntry>();
                cacheContainer.MemoryCache.Setup(m => m.CreateEntry(It.IsAny<string>())).Returns(cacheEntry.Object);
            }

            //Construct service
            TranslationsService = new TranslationsService(useRealCacheService ? cacheContainer.CacheService : CacheService.Object, RequestHandlerService.Object);
        }
    }
}
