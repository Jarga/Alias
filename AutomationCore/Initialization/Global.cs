using System;
using System.Collections;
using System.IO;
using AutomationCore.Output;
using AutomationCore.Selenium;
using AutomationCore.Shared;
using AutomationCore.Shared.Enumerations;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace AutomationCore.Initialization
{
    public static class Global
    {
        public static Environments TestEnvironment { get; set; }

        public static ITestableWebPage BaseTestPageType { get; set; }

        public static ITestOutput TestOutput { get; set; }

        public static void Initialize(ITestOutput testOutput)
        {
            TestOutput = testOutput;

//            string envMachine = DictionaryToString(Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Machine));
//            string envUser = DictionaryToString(Environment.GetEnvironmentVariables(EnvironmentVariableTarget.User));
//            string envProcess = DictionaryToString(Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process));
//            File.WriteAllText(@"C:\env_output.txt", $"Machine:{envMachine}\r\nUser:{envUser}\r\nProcess:{envProcess}");

            string environment = Environment.GetEnvironmentVariable("TestAutomationEnvironment", EnvironmentVariableTarget.Process);
            string browser = Environment.GetEnvironmentVariable("TestAutomationBrowser", EnvironmentVariableTarget.Process);

            if (environment != null)
            {
                switch (environment.ToLower())
                {
                    case "dev":
                        TestEnvironment = Environments.DEV;
                        break;

                    case "qa":
                        TestEnvironment = Environments.QA;
                        break;

                    case "prod":
                        TestEnvironment = Environments.PRODUCTION;
                        break;
                }
            }
            else
            {
                TestEnvironment = Environments.DEV;
            }

            if (browser != null)
            {
                switch (browser.ToLower())
                {
                    case "ie":
                    case "internetexplorer":
                        BaseTestPageType = new SeleniumWebPage(new InternetExplorerDriver(new InternetExplorerOptions(){ IgnoreZoomLevel = true }));
                        break;
                    case "chrome":
                    case "googlechrome":
                        BaseTestPageType = new SeleniumWebPage(new ChromeDriver());
                        break;
                    case "firefox":
                    case "mozillafirefox":
                        BaseTestPageType = new SeleniumWebPage(new FirefoxDriver());
                        break;
                }
            }
            else
            {
                BaseTestPageType = new SeleniumWebPage(new ChromeDriver());
            }
        }
        
        public static string DictionaryToString(IDictionary dictionary)
        {
            string result = string.Empty;

            foreach (var key in dictionary.Keys)
            {
                result += $"{key}={dictionary[key]}";
            }

            return result; ;
        }
    }
}
