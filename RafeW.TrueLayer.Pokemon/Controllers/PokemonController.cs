using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RafeW.TrueLayer.Pokemon.Engine.Entities.PokeAPI;
using RafeW.TrueLayer.Pokemon.Engine.Entities.Utilities;
using RafeW.TrueLayer.Pokemon.Engine.Exceptions;
using RafeW.TrueLayer.Pokemon.Engine.Services;
using RafeW.TrueLayer.Pokemon.Engine.Services.Api;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
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
        public async Task<IActionResult> Get([Required,MinLength(2)]string pokemonName)
        {
            try
            {
                var translatedText = await PokemonTranslationService.GetFlavourTextAsShakespearean(pokemonName);

                return new JsonResult(translatedText);
            }
            catch(PokemonTranslationException pokeEx)
            {
                if (pokeEx is PokemonTranslationApiException apiEx) {
                    //If the PokeAPI returned a 404, they gave us a pokemon name that doesn't exist, so our response is also a 404.
                    if (apiEx.SourceApi == PokeAPI_ApiSettings.Identifier && apiEx.HttpStatusCode == HttpStatusCode.NotFound) 
                        return NotFound(pokeEx.FriendlyMessage);

                    //If the translation API returned 429, pass it on.
                    if (apiEx.SourceApi == Translations_ApiSettings.Identifier && apiEx.HttpStatusCode == HttpStatusCode.TooManyRequests)
                        return StatusCode((int)HttpStatusCode.TooManyRequests, apiEx.FriendlyMessage);
                }

                return StatusCode((int)HttpStatusCode.InternalServerError, pokeEx.FriendlyMessage);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Sorry, an unknown error occurred");
            }
        }
    }
}
