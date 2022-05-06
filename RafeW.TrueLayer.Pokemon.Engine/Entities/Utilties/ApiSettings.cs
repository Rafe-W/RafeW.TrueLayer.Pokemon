using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafeW.TrueLayer.Pokemon.Engine.Entities.Utilities
{
    public interface IApiSettings
    {
        /// <summary>
        /// To be treated as a constant for retrieving API information from configuration files
        /// </summary>
        string ApiIdentifier { get; }
        Dictionary<string, string> DefaultHeaders { get; set; }
        Uri BaseUrl { get; set; }
    }

    public class PokeAPI_ApiSettings : IApiSettings
    {
        public const string ApiIdentifier = "PokeAPI";

        public Dictionary<string, string> DefaultHeaders { get; set; }
        public Uri BaseUrl { get; set; }

        string IApiSettings.ApiIdentifier => ApiIdentifier;
    }

    public class Translation_ApiSettings : IApiSettings
    {
        public const string ApiIdentifier = "Translation";

        public Dictionary<string, string> DefaultHeaders { get; set; }
        public Uri BaseUrl { get; set; }

        string IApiSettings.ApiIdentifier => ApiIdentifier;
    }
}
