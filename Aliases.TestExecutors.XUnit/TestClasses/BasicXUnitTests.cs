using System;
using Aliases.Common.Configuration;
using Xunit.Abstractions;

namespace Aliases.TestExecutors.XUnit.TestClasses
{
    public class BasicXUnitTests<T> : IDisposable where T : ITestConfiguration, new()
    {
        public ITestConfiguration TestConfiguration { get; set; }

        public BasicXUnitTests(ITestOutputHelper output)
        {
            TestConfiguration = new T().Create(new XUnitTestOutput(output));

            TestConfiguration.TestOutput.WriteLine($"Tests started with configuration: TestAutomationUrl: {TestConfiguration.BaseTestUrl}, TestAutomationEnvironment: {TestConfiguration.TestEnvironmentType}, TestAutomationBrowser: {TestConfiguration.Browser}");
        }

        public void Dispose()
        {
            TestConfiguration.Dispose();
        }
    }
}
