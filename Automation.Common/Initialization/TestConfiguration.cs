using System;
using System.Collections;
using Automation.Common.Drivers.Selenium;
using Automation.Common.Initialization.Interfaces;
using Automation.Common.Output;
using Automation.Common.Shared;
using Automation.Common.Shared.Enumerations;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace Automation.Common.Initialization
{
    public class TestConfiguration : ITestConfiguration
    {
        public EnvironmentType TestEnvironmentType { get; set; }

        public ITestableWebPage BaseTestPageType { get; set; }

        public ITestOutput TestOutput { get; set; }


        public TestConfiguration(EnvironmentType testEnvironmentType, ITestableWebPage baseTestPageType, ITestOutput testOutput, int actionTimeout)
        {
            TestEnvironmentType = testEnvironmentType;
            BaseTestPageType = baseTestPageType;
            TestOutput = testOutput;

            BaseTestPageType.DefaultActionTimeout = actionTimeout;
        }

        ITestConfiguration ITestConfiguration.Build(ITestOutput testOutput, EnvironmentType? testEnvironmentType, ITestableWebPage baseTestPageType)
        {
            return Build(testOutput, testEnvironmentType, baseTestPageType);
        }

        public static ITestConfiguration Build(ITestOutput testOutput, EnvironmentType? testEnvironmentType = null,  ITestableWebPage baseTestPageType = null)
        {

            //Set Test EnvironmentType
            if (!testEnvironmentType.HasValue)
            { 
                string environment = System.Environment.GetEnvironmentVariable("TestAutomationEnvironment", EnvironmentVariableTarget.Process);

                EnvironmentType environmentType;
                Enum.TryParse(environment, true, out environmentType);

                testEnvironmentType = environmentType;
            }

            //Set Page Type
            if (baseTestPageType == null)
            {
                string browser = System.Environment.GetEnvironmentVariable("TestAutomationBrowser", EnvironmentVariableTarget.Process);
                
                Browser browserType;
                Enum.TryParse(browser, true, out browserType);

                //TODO: We can pass the test output into the selenium web page for the ability to log all actions that are done on the driver
                IWebDriver driver = GetWebDriver(browserType);
                baseTestPageType = new SeleniumWebPage(driver);
            }

            return new TestConfiguration(testEnvironmentType.Value, baseTestPageType, testOutput, 60);
        }

        public static IWebDriver GetWebDriver(Browser browser)
        {
            IWebDriver driver;
            switch (browser)
            {
                case Browser.IE:
                    driver = StartIE();
                    break;
                case Browser.Firefox:
                    driver = StartFirefox();
                    break;
                default:
                    driver = StartChrome();
                    break;
            }

            driver.Manage().Cookies.DeleteAllCookies();

            return driver;
        }

        public static IWebDriver StartFirefox()
        {
            return new FirefoxDriver();
        }

        public static IWebDriver StartChrome()
        {
            return new ChromeDriver();
        }

        public static IWebDriver StartIE()
        {
            return new InternetExplorerDriver(new InternetExplorerOptions
            {
                IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                IgnoreZoomLevel = true
            });
        }

        public void Dispose()
        {
            BaseTestPageType.Dispose();
        }
    }
}
