using System;
using Alias.Common.Output;
using Alias.Common.Shared;
using Alias.Common.Shared.Enumerations;

namespace Alias.Common.Initialization.Interfaces
{
    public interface ITestConfiguration : IDisposable
    {
        string BaseTestUrl { get; set; }

        EnvironmentType? TestEnvironmentType { get; set; }

        ITestableWebPage BaseTestPageType { get; set; }
        Browser Browser { get; set; }

        ITestOutput TestOutput { get; set; }

        ITestConfiguration Rebuild(ITestOutput testOutput, string baseTestUrl = null, EnvironmentType? testEnvironmentType = null, ITestableWebPage baseTestPageType = null);
    }
}
