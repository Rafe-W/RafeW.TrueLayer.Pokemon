using System.Text.Json.Serialization;

namespace RafeW.TrueLayer.Pokemon.Api.Models.Pokemon
{
    public class TranslationResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
