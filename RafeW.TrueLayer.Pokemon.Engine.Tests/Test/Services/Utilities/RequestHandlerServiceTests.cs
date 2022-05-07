using Microsoft.VisualStudio.TestTools.UnitTesting;
using RafeW.TrueLayer.Pokemon.Engine.Tests.TestHelpers.ServiceContainers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafeW.TrueLayer.Pokemon.Engine.Tests.Test.Services.Utilities
{
    [TestClass]
    public class RequestHandlerServiceTests
    {
        [TestMethod]
        public void RequestHandlerService_Ctor_WithHeaders()
        {
            //Arrange
            var identifier = "Test";
            var baseUrl = "https://www.example.com/";
            var baseUrlAsUri = new Uri(baseUrl);
            var headerKey = "x-api-key";
            var headerValue = "abc123";
            var mockConfig = RequestHandlerServiceContainer.BuildMockConfiguration(identifier, baseUrl, new Dictionary<string, string>
            {
                {  headerKey, headerValue }
            });

            //Act
            //Container runs the constructor which configures from config
            var container = new RequestHandlerServiceContainer(mockConfig);
            var service = container.RequestHandlerService;

            //Assert
            Assert.IsNotNull(service.Settings);
            Assert.AreEqual(baseUrlAsUri, container.RequestHandlerService.Settings.BaseUrl);
            Assert.AreEqual(1, service.Settings.DefaultHeaders.Count);
            Assert.AreEqual(headerKey, service.Settings.DefaultHeaders.First().Key);
            Assert.AreEqual(headerValue, service.Settings.DefaultHeaders.First().Value);
        }

        [TestMethod]
        public void RequestHandlerService_Ctor_WithoutHeaders()
        {
            //Arrange
            var identifier = "Test";
            var baseUrl = "https://www.example.com/";
            var baseUrlAsUri = new Uri(baseUrl);
            var mockConfig = RequestHandlerServiceContainer.BuildMockConfiguration(identifier, baseUrl, null);

            //Act
            //Container runs the constructor which configures from config
            var container = new RequestHandlerServiceContainer(mockConfig);
            var service = container.RequestHandlerService;

            //Assert
            Assert.IsNotNull(service.Settings);
            Assert.AreEqual(baseUrlAsUri, container.RequestHandlerService.Settings.BaseUrl);
            Assert.AreEqual(0, service.Settings.DefaultHeaders.Count);
        }
    }
}
