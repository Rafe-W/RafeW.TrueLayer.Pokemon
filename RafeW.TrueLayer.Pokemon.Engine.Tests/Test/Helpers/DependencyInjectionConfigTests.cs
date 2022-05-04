using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RafeW.TrueLayer.Pokemon.Engine.Helpers;
using RafeW.TrueLayer.Pokemon.Engine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafeW.TrueLayer.Pokemon.Engine.Tests.Test.Helpers
{
    [TestClass]
    public class DependencyInjectionConfigTests
    {

        [TestMethod]
        public void AddPokemonEngine_Injects()
        {
            //Arrange
            var serviceCollection = new ServiceCollection();
            //Add external dependencies
            serviceCollection.AddMemoryCache();

            //Act
            serviceCollection.AddPokemonEngine();
            var provider = serviceCollection.BuildServiceProvider();

            //Assert
            var scope = provider.CreateScope();
            //Test retreiving a service - PokemonTranslationService used as this is the outermost service at this time.
            var pokemonTranslationService = scope.ServiceProvider.GetService<IPokemonTranslationService>();

            Assert.IsNotNull(pokemonTranslationService);
        }
    }
}
