using System;
using Automation.Common.Initialization.Interfaces;
using Automation.Common.Output.XUnit;
using Xunit.Abstractions;

namespace Automation.Common.Executors.XUnit.TestClasses
{
    public class BasicXUnitTests : IDisposable
    {
        public ITestConfiguration TestConfiguration { get; set; }

        public BasicXUnitTests(ITestOutputHelper output)
        {
            TestConfiguration = Initialization.TestConfiguration.Build(new XUnitTestOutput(output));

            TestConfiguration.TestOutput.WriteLine($"Tests started with configuration: TestAutomationUrl: {TestConfiguration.BaseTestUrl}, TestAutomationEnvironment: {TestConfiguration.TestEnvironmentType}, TestAutomationBrowser: {TestConfiguration.Browser}");
        }

        public void Dispose()
        {
            TestConfiguration.Dispose();
        }
    }
}
