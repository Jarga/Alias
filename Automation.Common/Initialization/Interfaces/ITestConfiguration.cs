using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common.Output;
using Automation.Common.Shared;
using Automation.Common.Shared.Enumerations;

namespace Automation.Common.Initialization.Interfaces
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
