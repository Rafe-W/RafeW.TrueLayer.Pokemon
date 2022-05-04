using Microsoft.Extensions.DependencyInjection;
using RafeW.TrueLayer.Pokemon.Engine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafeW.TrueLayer.Pokemon.Engine.Helpers
{
    public static class DependencyInjectionConfig
    {
        public static void AddPokemonEngine(this IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(scan => scan.FromAssemblyOf<PokemonTranslationService>().AddClasses(c => c.WithAttribute(typeof(InjectableAttribute))).AsImplementedInterfaces().WithScopedLifetime());
        }
    }
}
