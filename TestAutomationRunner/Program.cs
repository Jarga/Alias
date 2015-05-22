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
            Environment.SetEnvironmentVariable("TestAutomationEnvironment", "dev");
            Environment.SetEnvironmentVariable("TestAutomationBrowser", "firefox");

            Xunit.ConsoleClient.Program.Main(new[] { "TestAutomation.dll", "-trait", "Suite=CreateUser", "-html", "C:\\output.html" });
        }
    }
}
