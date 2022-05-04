using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RafeW.TrueLayer.Pokemon.Engine.Tests.TestHelpers.ServiceContainers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafeW.TrueLayer.Pokemon.Engine.Tests.Test.Services.Utilities
{
    [TestClass]
    public class CacheServiceTests
    {
        [TestMethod]
        public void ProcessCaching_AddsToCache()
        {
            //Arrange
            var cacheKey = "TestCacheKey";
            var valueToCache = 1;
            
            var cacheServiceContainer = new CacheServiceContainer();
            var cacheTestService = new CacheTestService<int>(valueToCache);
            var cacheEntry = new Mock<ICacheEntry>();
            var cacheTimeSpan = TimeSpan.FromMinutes(5);
            //Set up mock on memory cache (need throwaway for moq) so that TryGetValue returns false.
            var throwawayObject = new object();
            cacheServiceContainer.MemoryCache.Setup(m => m.TryGetValue(cacheKey, out throwawayObject)).Returns(false);
            cacheServiceContainer.MemoryCache.Setup(m => m.CreateEntry(cacheKey)).Returns(cacheEntry.Object);

            var service = cacheServiceContainer.CacheService;

            //Act
            var result =  service.ProcessCaching(cacheKey, () => cacheTestService.GetReturnValue(), cacheTimeSpan);

            //Assert
            Assert.AreEqual(valueToCache, result);
            cacheServiceContainer.MemoryCache.Verify(m => m.TryGetValue(cacheKey, out throwawayObject), Times.Once);
            cacheServiceContainer.MemoryCache.Verify(m => m.CreateEntry(cacheKey), Times.Once);
            Assert.IsTrue(cacheTestService.WasCalled);
            Assert.AreEqual(1, cacheTestService.CallCount);
        }

        [TestMethod]
        public void ProcessCaching_RetrievesFromCache()
        {
            //Arrange
            var cacheKey = "TestCacheKey";
            object valueToCache = 1; //Moq requires the value to be "object" or it won't set up correctly

            var cacheServiceContainer = new CacheServiceContainer();
            var cacheTestService = new CacheTestService<int>((int)valueToCache);
            var cacheTimeSpan = TimeSpan.FromMinutes(5);

            //Set up mock on memory cache so that TryGetValue returns true.
            cacheServiceContainer.MemoryCache.Setup(m => m.TryGetValue(cacheKey, out valueToCache)).Returns(true);
            var service = cacheServiceContainer.CacheService;

            //Act
            var result = service.ProcessCaching(cacheKey, () => cacheTestService.GetReturnValue(), cacheTimeSpan);

            //Assert
            Assert.AreEqual(valueToCache, result);
            cacheServiceContainer.MemoryCache.Verify(m => m.TryGetValue(cacheKey, out valueToCache), Times.Once);
            cacheServiceContainer.MemoryCache.Verify(m => m.CreateEntry(cacheKey), Times.Never);
            Assert.IsFalse(cacheTestService.WasCalled);
        }

        /// <summary>
        /// Test service for the CacheService unit tests to mock process of a service method retreiving a value that must be cached
        /// </summary>
        public class CacheTestService<T> : ICacheTestService<T>
        {
            private readonly T _returnValue;
            private int _callCount;

            public bool WasCalled => CallCount > 0;
            public int CallCount => _callCount;

            public CacheTestService(T returnValue)
            {
                _returnValue = returnValue;
            }

            public T GetReturnValue()
            {
                _callCount++;
                return _returnValue;
            }
        }
        public interface ICacheTestService<T>
        {
            T GetReturnValue();
        }
    }
}
