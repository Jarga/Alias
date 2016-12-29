using Aliases.Common.Configuration;
using Aliases.Drivers.Selenium.Configuration;
using Aliases.Example.CustomConfigs;
using Aliases.TestExecutors.XUnit;
using Aliases.TestExecutors.XUnit.TestClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Aliases.Example.TestTypes
{
    public class SeleniumXUnitTests : IDisposable
    {
        public ITestConfiguration TestConfiguration { get; set; }

        public SeleniumXUnitTests(ITestOutputHelper output)
        {
            TestConfiguration = new CustomSeleniumConfiguration().Create(new XUnitTestOutput(output));

            TestConfiguration.TestOutput.WriteLine($"Tests started with configuration: TestAutomationUrl: {TestConfiguration.BaseTestUrl}, TestAutomationEnvironment: {TestConfiguration.TestEnvironmentType}, TestAutomationBrowser: {TestConfiguration.Browser}");
        }

        public void Dispose()
        {
            TestConfiguration.Dispose();
        }
    }
}
