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
    public class PokeAPIServiceContainer
    {
        public Mock<ICacheService> CacheService { get; set; }
        public Mock<IRequestHandlerService<PokeAPI_ApiSettings>> RequestHandlerService { get; set; }
        public PokeAPIService PokeAPIService { get; set; }

        public PokeAPIServiceContainer()
        {
            //Construct Mocks
            CacheService = new Mock<ICacheService>();
            RequestHandlerService = new Mock<IRequestHandlerService<PokeAPI_ApiSettings>>();

            //Construct service
            PokeAPIService = new PokeAPIService(CacheService.Object, RequestHandlerService.Object);
        }
    }
}
