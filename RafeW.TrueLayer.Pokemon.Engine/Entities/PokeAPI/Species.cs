using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RafeW.TrueLayer.Pokemon.Engine.Entities.PokeAPI
{
    //NB: Only the relevant information is mapped
    public class Species
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("flavor_text_entries")]
        public List<FlavourText> FlavourTextEntries { get; set; }
    }

    public class FlavourText
    {
        [JsonPropertyName("flavor_text")]
        public string Text { get; set; }
        [JsonPropertyName("language")]
        public Language Language { get; set; }
    }

    public class Language
    {
        public const string EnglishLanguageIdentifier = "en";
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
