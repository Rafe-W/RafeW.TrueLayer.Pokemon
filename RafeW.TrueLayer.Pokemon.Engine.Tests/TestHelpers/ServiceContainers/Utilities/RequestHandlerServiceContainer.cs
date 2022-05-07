using Microsoft.Extensions.Configuration;
using Moq;
using RafeW.TrueLayer.Pokemon.Engine.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafeW.TrueLayer.Pokemon.Engine.Tests.TestHelpers.ServiceContainers.Utilities
{
    public class RequestHandlerServiceContainer
    {
        public Mock<IConfiguration> Configuration { get; }

        public RequestHandlerService<TestApiSettings> RequestHandlerService { get; }

        public RequestHandlerServiceContainer(Mock<IConfiguration> mock)
        {
            //Since this service has logic in the constructor for getting the values out of config, the Mock configuration should be passed in with expected values.
            RequestHandlerService = new RequestHandlerService<TestApiSettings>(mock.Object);
        }

        public static Mock<IConfiguration> BuildMockConfiguration(string settingsIdentifier, string baseUrl, Dictionary<string, string> defaultHeaders = null)
        {
            var mock = new Mock<IConfiguration>();
            var settingsConfigMock = new Mock<IConfigurationSection>();
            var externalConfigMock = new Mock<IConfigurationSection>();
            var headersConfigMock = new Mock<IConfigurationSection>();
            var headersSection = new List<IConfigurationSection>();

            if (defaultHeaders != null)
            {
                foreach (var header in defaultHeaders)
                {
                    var mockSection = new Mock<IConfigurationSection>();
                    mockSection.SetupGet(s => s.Key).Returns(header.Key);
                    mockSection.SetupGet(s => s.Value).Returns(header.Value);
                    headersSection.Add(mockSection.Object);
                }
            }

            settingsConfigMock.Setup(s => s["BaseUrl"]).Returns(baseUrl);
            externalConfigMock.Setup(e => e.GetSection(settingsIdentifier)).Returns(settingsConfigMock.Object);

            //If default headers isn't present in config, these should return null (default behaviour when not set)
            if (defaultHeaders != null)
            {
                settingsConfigMock.Setup(s => s.GetSection("DefaultHeaders")).Returns(headersConfigMock.Object);
                headersConfigMock.Setup(s => s.GetChildren()).Returns(headersSection);
            }

            mock.Setup(c => c.GetSection("ExternalApiConfig")).Returns(externalConfigMock.Object);

            return mock;
        }
    }
}
