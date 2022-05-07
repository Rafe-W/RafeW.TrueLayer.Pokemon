using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafeW.TrueLayer.Pokemon.Engine.Exceptions
{
    /// <summary>
    /// For general exceptions during translation process
    /// </summary>
    public class PokemonTranslationException : Exception
    {
        public PokemonTranslationException(string pokemonName, string friendlyMessage, Exception innerException) : base(friendlyMessage, innerException)
        {
            PokemonName = pokemonName;
            FriendlyMessage = friendlyMessage;
        }

        public string PokemonName { get; }
        public string FriendlyMessage { get; }
    }
}
