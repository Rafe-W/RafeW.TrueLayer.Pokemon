using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RafeW.TrueLayer.Pokemon.Engine.Exceptions
{
    /// <summary>
    /// For exceptions when calling an API used in the process of translating.
    /// </summary>
    public class PokemonTranslationApiException : PokemonTranslationException
    {
        public string SourceApi { get; }
        public HttpStatusCode HttpStatusCode { get; }
        public const string GenericFriendlyMessage = "Sorry, we couldn't process that request as we encountered an error.";

        public const string TooManyRequestsFriendlyMessage = "Sorry, we can't process that request right now due to too many requests. Please try again later";

        public PokemonTranslationApiException(string sourceApi, HttpStatusCode httpStatusCode, string friendlyMessage, Exception innerException, string pokemonName = "") : base(pokemonName, friendlyMessage, innerException)
        {
            SourceApi = sourceApi;
            HttpStatusCode = httpStatusCode;
        }
    }
}
