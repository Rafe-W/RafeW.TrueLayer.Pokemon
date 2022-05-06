using RafeW.TrueLayer.Pokemon.Engine.Entities.PokeAPI;
using RafeW.TrueLayer.Pokemon.Engine.Entities.Utilities;
using RafeW.TrueLayer.Pokemon.Engine.Helpers;
using RafeW.TrueLayer.Pokemon.Engine.Services.Utilities;
using System;
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
            return await CacheService.ProcessCaching($"PokeAPI:Species:{speciesName}", async () =>
            {
                var requestResult = await RequestHandlerService.TrySendRequest<Species>($"pokemon-species/{speciesName}/", HttpMethod.Get);
                if (requestResult.Success)
                    return requestResult.Response;
                return null; //Throw specific errors from here based on result exception (aka 404, couldn't connect)
            }, TimeSpan.FromMinutes(10));
        }
    }
}
