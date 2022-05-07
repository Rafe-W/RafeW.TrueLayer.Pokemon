using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RafeW.TrueLayer.Pokemon.Engine.Entities.Translations
{
    public class TranslationResult
    {
        [JsonPropertyName("success")]
        public TranslationResult_Success Success { get; set; }
        [JsonPropertyName("contents")]
        public TranslationsResult_Contents Contents { get; set; }
    }

    public class TranslationResult_Success
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }

    public class TranslationsResult_Contents
    {
        [JsonPropertyName("translated")]
        public string Translated { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("translation")]
        public string Translation { get; set; }
    }
}
