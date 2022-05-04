using Moq;
using RafeW.TrueLayer.Pokemon.Engine.Services;
using RafeW.TrueLayer.Pokemon.Engine.Services.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafeW.TrueLayer.Pokemon.Engine.Tests.TestHelpers.ServiceContainers
{
    public class PokemonTranslationServiceContainer
    {
        public Mock<IPokeAPIService> PokeAPIService { get; }
        public Mock<ITranslationsService> TranslationsService { get; }

        public PokemonTranslationService PokemonTranslationService { get; }

        public PokemonTranslationServiceContainer()
        {
            //Construct mocks
            PokeAPIService = new Mock<IPokeAPIService>();
            TranslationsService = new Mock<ITranslationsService>();

            //Construct service
            PokemonTranslationService = new PokemonTranslationService(PokeAPIService.Object, TranslationsService.Object);
        }
    }
}
