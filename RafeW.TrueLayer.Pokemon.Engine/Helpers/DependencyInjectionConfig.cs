using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafeW.TrueLayer.Pokemon.Engine.Helpers
{
    public static class DependencyInjectionConfig
    {
        public static void InjectPokemonEngine(IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(scan => scan.FromAssemblyOf<InjectableAttribute>().AddClasses(c => c.WithAttribute(typeof(InjectableAttribute))).AsImplementedInterfaces().WithScopedLifetime());
        }
    }
}
