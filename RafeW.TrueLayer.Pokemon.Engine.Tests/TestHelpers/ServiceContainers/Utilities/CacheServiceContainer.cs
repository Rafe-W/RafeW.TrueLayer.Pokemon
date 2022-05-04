using Microsoft.Extensions.Caching.Memory;
using Moq;
using RafeW.TrueLayer.Pokemon.Engine.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafeW.TrueLayer.Pokemon.Engine.Tests.TestHelpers.ServiceContainers.Utilities
{
    public class CacheServiceContainer
    {
        public Mock<IMemoryCache> MemoryCache { get;  }
        public CacheService CacheService { get; }

        public CacheServiceContainer()
        {
            //Construct Mocks
            MemoryCache = new Mock<IMemoryCache>();

            //Construct service
            CacheService = new CacheService(MemoryCache.Object);
        }
    }
}
