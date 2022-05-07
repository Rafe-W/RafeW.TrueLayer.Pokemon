using Moq;
using RafeW.TrueLayer.Pokemon.Engine.Entities.Utilities;
using RafeW.TrueLayer.Pokemon.Engine.Services.Api;
using RafeW.TrueLayer.Pokemon.Engine.Services.Utilities;
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

        public TranslationsServiceContainer()
        {
            //Construct Mocks
            CacheService = new Mock<ICacheService>();
            RequestHandlerService = new Mock<IRequestHandlerService<Translations_ApiSettings>>();

            //Construct service
            TranslationsService = new TranslationsService(CacheService.Object, RequestHandlerService.Object);
        }
    }
}
