using System;
using AutomationCore.Selenium;
using AutomationCore.Shared;
using AutomationCore.Shared.Enumerations;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace AutomationCore.Initialization
{
    public static class GlobalTestSettings
    {
        public static Environments TestEnvironment { get; set; }

        public static ITestableWebPage BaseTestPageType { get; set; }
        
        public static void Initialize()
        {
            string environment = Environment.GetEnvironmentVariable("TestAutomationEnvironment");
            string browser = Environment.GetEnvironmentVariable("TestAutomationBrowser");

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
    }
}
