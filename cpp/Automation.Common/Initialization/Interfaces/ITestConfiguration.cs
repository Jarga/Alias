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
        EnvironmentType TestEnvironmentType { get; set; }

        ITestableWebPage BaseTestPageType { get; set; }

        ITestOutput TestOutput { get; set; }
        
        ITestConfiguration Build(ITestOutput testOutput, EnvironmentType? testEnvironmentType = null, ITestableWebPage baseTestPageType = null);
    }
}
