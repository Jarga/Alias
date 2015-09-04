using System;
using AutomationCore.Initialization;
using AutomationCore.Output.XUnit;
using Xunit.Abstractions;

namespace AutomationCore.TestClasses
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
