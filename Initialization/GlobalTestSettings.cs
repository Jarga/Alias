using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using TestAutomation.Selenium;
using TestAutomation.Shared;
using TestAutomation.Shared.Enumerations;

namespace TestAutomation.Initialization
{
    public static class GlobalTestSettings
    {
        public static Environments TestEnvironment { get; set; }

        public static ITestableWebPage BaseTestPageType { get; set; }

        static GlobalTestSettings()
        {
            string environment = Environment.GetEnvironmentVariable("TestAutomationEnvironment");
            string browser = Environment.GetEnvironmentVariable("TestAutomationBrowser");

            if (environment != null)
            {
                switch (environment)
                {
                    case "Dev":
                        TestEnvironment = Environments.DEV;
                        break;

                    case "QA":
                        TestEnvironment = Environments.QA;
                        break;
                }
            }
            else
            {
                TestEnvironment = Environments.DEV;
            }

            if (browser != null)
            {
                switch (browser)
                {
                    case "InternetExplorer":
                        BaseTestPageType = new SeleniumWebPage(new InternetExplorerDriver(new InternetExplorerOptions() {IgnoreZoomLevel = true}));
                        break;
                }
            }
            else
            {
                BaseTestPageType = new SeleniumWebPage(new ChromeDriver());
            }
        }
    }
}
