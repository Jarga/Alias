using System;
using System.Threading.Tasks;

namespace TestAutomationRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("TestAutomationEnvironment", "qa");
            Environment.SetEnvironmentVariable("TestAutomationBrowser", "chrome");

            Xunit.ConsoleClient.Program.Main(new[] { "TestAutomation.dll", "-namespace", "FunctionalTests.MarketOnce", "-html", "output.html", "-parallel", "none" });
        }
    }
}
