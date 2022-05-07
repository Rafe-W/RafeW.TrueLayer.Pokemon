using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RafeW.TrueLayer.Pokemon.Engine.Entities.PokeAPI;
using RafeW.TrueLayer.Pokemon.Engine.Services;
using RafeW.TrueLayer.Pokemon.Engine.Services.Api;
using System.Linq;
using System.Threading.Tasks;

namespace RafeW.TrueLayer.Pokemon.Api.Controllers
{
    [Route("api/pokemon")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        public IPokemonTranslationService PokemonTranslationService { get; }
        public PokemonController(IPokemonTranslationService pokemonTranslationService)
        {
            PokemonTranslationService = pokemonTranslationService;
        }

        [HttpGet, Route("{pokemonName}")]
        public async Task<IActionResult> Get(string pokemonName)
        {
            var translatedText = await PokemonTranslationService.GetFlavourTextAsShakespearean(pokemonName);

            return new JsonResult(translatedText);
        }
    }
}
