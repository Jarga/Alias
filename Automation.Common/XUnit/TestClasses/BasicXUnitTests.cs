using System;
using Automation.Common.Initialization;
using Automation.Common.Output.XUnit;
using Xunit.Abstractions;

namespace Automation.Common.XUnit.TestClasses
{
    public class BasicXUnitTests : IDisposable
    {
        public BasicXUnitTests(ITestOutputHelper output)
        {
            Global.Initialize(new XUnitTestOutput(output));
        }

        public void Dispose()
        {
            Global.BaseTestPageType.Close();
        }
    }
}
