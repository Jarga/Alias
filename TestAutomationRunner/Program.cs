using System;
using System.Threading.Tasks;

namespace TestAutomationRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("TestAutomationEnvironment", "dev");
            Environment.SetEnvironmentVariable("TestAutomationBrowser", "chrome");

            Xunit.ConsoleClient.Program.Main(new[] { "TestAutomation.dll", "-partialname", "TestAutomation.FunctionalTests.MarketOnce.Admin.Users", "-xml", "output.xml", "-html", "output.html", "-parallel", "none" });
        }
    }
}
