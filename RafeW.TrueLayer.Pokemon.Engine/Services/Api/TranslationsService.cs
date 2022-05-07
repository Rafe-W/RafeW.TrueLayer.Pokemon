using RafeW.TrueLayer.Pokemon.Engine.Entities.Translations;
using RafeW.TrueLayer.Pokemon.Engine.Entities.Utilities;
using RafeW.TrueLayer.Pokemon.Engine.Exceptions;
using RafeW.TrueLayer.Pokemon.Engine.Helpers;
using RafeW.TrueLayer.Pokemon.Engine.Services.Utilities;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RafeW.TrueLayer.Pokemon.Engine.Services.Api
{
    public interface ITranslationsService
    {
        Task<string> ToShakespearean(string text);
    }

    [Injectable]
    public class TranslationsService : ITranslationsService
    {
        public Regex FormatRegex { get; set; } = new Regex("(\n)|(\f)");
        private ICacheService CacheService { get; }
        private IRequestHandlerService<Translations_ApiSettings> RequestHandlerService { get; }
        public TranslationsService(ICacheService cacheService, IRequestHandlerService<Translations_ApiSettings> requestHandlerService)
        {
            CacheService = cacheService;
            RequestHandlerService = requestHandlerService;
        }

        public async Task<string> ToShakespearean(string text)
        {
            var formatText = FormatRegex.Replace(text, " ").Replace("  ", " "); //Replace any double spaces created as a result
            var translationKey = GetStringBase64(formatText);
            var translationResult = await CacheService.ProcessCachingAsync($"Translations:Shakespeare:{translationKey}", async () =>
                {
                    var result = await RequestHandlerService.TrySendRequest<TranslationResult>($"shakespeare.json?text={WebUtility.UrlEncode(formatText)}", HttpMethod.Post);
                    if (result.Success)
                        return result.Response;

                    if (result.Exception is HttpRequestException httpEx)
                    {
                        string message = PokemonTranslationApiException.GenericFriendlyMessage;
                        switch (httpEx.StatusCode)
                        {
                            case HttpStatusCode.TooManyRequests:
                                message = PokemonTranslationApiException.TooManyRequestsFriendlyMessage;
                                break;
                            default:
                                break;
                        }

                        throw new PokemonTranslationApiException(RequestHandlerService.Settings.ApiIdentifier, httpEx.StatusCode.Value, message, result.Exception);
                    }

                    throw new PokemonTranslationException("", "Sorry, an unknown error occurred", result.Exception);
                }, TimeSpan.FromMinutes(10));

            if (translationResult.Success.Total == 0)
                throw new PokemonTranslationException("", "Sorry, the translation was unsuccessful", null);
            return translationResult.Contents.Translated;
        }

        private string GetStringBase64(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(bytes);
        }
    }
}
