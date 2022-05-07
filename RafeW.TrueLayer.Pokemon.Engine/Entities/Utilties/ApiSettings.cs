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

    public abstract class ApiSettingsBase : IApiSettings
    {
        public Dictionary<string, string> DefaultHeaders { get; set; }
        public Uri BaseUrl { get; set; }

        public abstract string ApiIdentifier { get; }
    }

    public class PokeAPI_ApiSettings : ApiSettingsBase
    {
        public const string Identifier = "PokeAPI";

        public override string ApiIdentifier => Identifier;
    }

    public class Translations_ApiSettings : ApiSettingsBase
    {
        public const string Identifier = "Translations";

        public override string ApiIdentifier => Identifier;
    }
}
