using RafeW.TrueLayer.Pokemon.Engine.Helpers;
using RafeW.TrueLayer.Pokemon.Engine.Services.Api;

namespace RafeW.TrueLayer.Pokemon.Engine.Services
{
    public interface IPokemonTranslationService
    {

    }

    [Injectable]
    public class PokemonTranslationService
    {
        private IPokeAPIService PokeAPIService { get; }
        private ITranslationsService TranslationsService { get; }
        public PokemonTranslationService(IPokeAPIService pokeAPIService, ITranslationsService translationsService)
        {
            PokeAPIService = pokeAPIService;
            TranslationsService = translationsService;
        }
    }
}
