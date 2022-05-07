using RafeW.TrueLayer.Pokemon.Engine.Entities.PokeAPI;
using RafeW.TrueLayer.Pokemon.Engine.Entities.Utilities;
using RafeW.TrueLayer.Pokemon.Engine.Exceptions;
using RafeW.TrueLayer.Pokemon.Engine.Helpers;
using RafeW.TrueLayer.Pokemon.Engine.Services.Utilities;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RafeW.TrueLayer.Pokemon.Engine.Services.Api
{
    public interface IPokeAPIService
    {
        Task<Species> GetSpeciesData(string speciesName);
    }

    [Injectable]
    public class PokeAPIService : IPokeAPIService
    {
        private ICacheService CacheService { get; }
        private IRequestHandlerService<PokeAPI_ApiSettings> RequestHandlerService { get; }

        public PokeAPIService(ICacheService cacheService, IRequestHandlerService<PokeAPI_ApiSettings> requestHandlerService)
        {
            CacheService = cacheService;
            RequestHandlerService = requestHandlerService;
        }

        public async Task<Species> GetSpeciesData(string speciesName)
        {
            return await CacheService.ProcessCachingAsync($"PokeAPI:Species:{speciesName.ToLower()}", async () =>
            {
                var requestResult = await RequestHandlerService.TrySendRequest<Species>($"pokemon-species/{speciesName}/", HttpMethod.Get);
                if (requestResult.Success)
                    return requestResult.Response;

                if (requestResult.Exception is HttpRequestException httpEx)
                {
                    string message = PokemonTranslationApiException.GenericFriendlyMessage;
                    switch (httpEx.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            message = $"Sorry, we couldn't find a pokemon species called {speciesName}";
                            break;
                        default:
                            break;
                    }

                    throw new PokemonTranslationApiException(RequestHandlerService.Settings.ApiIdentifier, httpEx.StatusCode.Value, message, requestResult.Exception);
                }
                
                throw new PokemonTranslationException(speciesName, "Sorry, an unknown error occurred", requestResult.Exception);
            }, TimeSpan.FromMinutes(10));
        }
    }
}
