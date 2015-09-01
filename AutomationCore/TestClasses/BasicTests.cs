using System;
using AutomationCore.Initialization;
using Xunit.Abstractions;

namespace AutomationCore.TestClasses
{
    public class BasicTests : IDisposable
    {
        private readonly ITestOutputHelper output;

        public BasicTests(ITestOutputHelper output)
        {
            this.output = output;
            GlobalTestSettings.Initialize();
        }

        public void Dispose()
        {
            GlobalTestSettings.BaseTestPageType.Close();
        }
    }
}
