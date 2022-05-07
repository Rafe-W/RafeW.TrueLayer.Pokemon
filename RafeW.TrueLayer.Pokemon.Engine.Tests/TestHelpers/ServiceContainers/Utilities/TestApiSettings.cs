using RafeW.TrueLayer.Pokemon.Engine.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafeW.TrueLayer.Pokemon.Engine.Tests.TestHelpers.ServiceContainers.Utilities
{
    public class TestApiSettings : ApiSettingsBase
    {
        public const string Identifier = "Test";

        public override string ApiIdentifier => Identifier;
    }
}
