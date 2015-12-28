using System;
using Alias.Common.Initialization.Interfaces;
using Alias.Common.Output.XUnit;
using Xunit.Abstractions;

namespace Alias.Common.Executors.XUnit.TestClasses
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
