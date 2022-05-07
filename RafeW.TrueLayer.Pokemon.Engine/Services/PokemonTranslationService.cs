using RafeW.TrueLayer.Pokemon.Engine.Entities.PokeAPI;
using RafeW.TrueLayer.Pokemon.Engine.Helpers;
using RafeW.TrueLayer.Pokemon.Engine.Services.Api;
using System.Linq;
using System.Threading.Tasks;

namespace RafeW.TrueLayer.Pokemon.Engine.Services
{
    public interface IPokemonTranslationService
    {
        Task<string> GetFlavourTextAsShakespearean(string pokemonName);
    }

    [Injectable]
    public class PokemonTranslationService : IPokemonTranslationService
    {
        private IPokeAPIService PokeAPIService { get; }
        private ITranslationsService TranslationsService { get; }
        public PokemonTranslationService(IPokeAPIService pokeAPIService, ITranslationsService translationsService)
        {
            PokeAPIService = pokeAPIService;
            TranslationsService = translationsService;
        }

        public async Task<string> GetFlavourTextAsShakespearean(string pokemonName)
        {
            var species = await PokeAPIService.GetSpeciesData(pokemonName);
            //Get the first flavour text in english.
            var flavourText = species.FlavourTextEntries.FirstOrDefault(s => s.Language.Name == Language.EnglishLanguageIdentifier);

            var translated = await TranslationsService.ToShakespearean(flavourText.Text);

            return translated;
        }
    }
}
