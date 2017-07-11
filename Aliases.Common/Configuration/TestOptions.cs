using Aliases.Common.Output;
using Aliases.Common.Shared;
using Aliases.Common.Shared.Enumerations;

namespace Aliases.Common.Configuration
{
    public class TestOptions
    {
        public string BaseTestUrl { get; set; }

        public EnvironmentType? TestEnvironmentType { get; set; }

        public ITestableWebPage BaseTestPageType { get; set; }

        public Browser Browser { get; set; }

        public ITestOutput TestOutput { get; set; }

        public int ActionTimeout { get; set; }
    }
}
