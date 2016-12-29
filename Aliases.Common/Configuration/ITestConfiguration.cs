using System;
using Aliases.Common.Output;
using Aliases.Common.Shared;
using Aliases.Common.Shared.Enumerations;

namespace Aliases.Common.Configuration
{
    public interface ITestConfiguration : IDisposable, ICloneable
    {
        string BaseTestUrl { get; set; }

        EnvironmentType? TestEnvironmentType { get; set; }

        ITestableWebPage BaseTestPageType { get; set; }

        Browser Browser { get; set; }

        ITestOutput TestOutput { get; set; }

        ITestConfiguration Create(ITestOutput testOutput, string baseTestUrl = null, EnvironmentType? testEnvironmentType = null, ITestableWebPage baseTestPageType = null);
    }
}
