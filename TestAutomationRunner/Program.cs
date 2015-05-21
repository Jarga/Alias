using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomationRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("TestAutomationEnvironment", "Dev");
            Environment.SetEnvironmentVariable("TestAutomationBrowser", "InternetExplorer");

            Xunit.ConsoleClient.Program.Main(new[] { "TestAutomation.dll", "-trait", "Suite=EditTarget", "-html", "output.html", "-debug", "-wait" });
        }
    }
}
