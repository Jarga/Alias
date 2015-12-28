using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common.Drivers.Selenium;
using Automation.Common.Initialization.Interfaces;
using Automation.Common.Output;
using Automation.Common.Shared;
using Automation.Common.Shared.Enumerations;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Automation.Common.Initialization
{
    public class TestConfiguration : ITestConfiguration
    {
        public string BaseTestUrl { get; set; }

        public EnvironmentType? TestEnvironmentType { get; set; }

        public ITestableWebPage BaseTestPageType { get; set; }

        public Browser Browser { get; set; }

        public ITestOutput TestOutput { get; set; }
        
        public TestConfiguration(string baseTestUrl, EnvironmentType? testEnvironmentType, ITestableWebPage baseTestPageType, ITestOutput testOutput, int actionTimeout)
        {
            BaseTestUrl = baseTestUrl;
            TestEnvironmentType = testEnvironmentType;
            BaseTestPageType = baseTestPageType;
            TestOutput = testOutput;

            BaseTestPageType.DefaultActionTimeout = actionTimeout;
        }

        /// <summary>
        /// Builds the test configuration based on either environment variables or app.config setup
        /// 
        /// Looks for process level environment variables first, if those do not exist it will use the app.config entries
        /// </summary>
        /// <param name="testOutput"></param>
        /// <param name="baseTestUrl"></param>
        /// <param name="testEnvironmentType"></param>
        /// <param name="baseTestPageType"></param>
        /// <returns></returns>
        public static ITestConfiguration Build(ITestOutput testOutput, string baseTestUrl = null, EnvironmentType? testEnvironmentType = null, ITestableWebPage baseTestPageType = null)
        {
            //Set Test base url
            if (string.IsNullOrWhiteSpace(baseTestUrl))
            {
                baseTestUrl = Environment.GetEnvironmentVariable("TestAutomationUrl", EnvironmentVariableTarget.Process) ?? ConfigurationManager.AppSettings["TestAutomationUrl"];
            }

            //Set Test EnvironmentType
            if (!testEnvironmentType.HasValue)
            {
                string environment = Environment.GetEnvironmentVariable("TestAutomationEnvironment", EnvironmentVariableTarget.Process) ?? ConfigurationManager.AppSettings["TestAutomationEnvironment"];

                EnvironmentType environmentType;
                Enum.TryParse(environment, true, out environmentType);

                testEnvironmentType = environmentType;
            }
            
            Browser browserType = Browser.Chrome;

            //Set Page Type
            if (baseTestPageType == null)
            {
                string browser = Environment.GetEnvironmentVariable("TestAutomationBrowser", EnvironmentVariableTarget.Process) ?? ConfigurationManager.AppSettings["TestAutomationBrowser"];

                Enum.TryParse(browser, true, out browserType);

                IWebDriver driver = GetWebDriver(browserType);
                baseTestPageType = new SeleniumWebPage(driver);
            }
            
            return new TestConfiguration(baseTestUrl, testEnvironmentType, baseTestPageType, testOutput, 60)
            {
                Browser = browserType
            };
        }

        public ITestConfiguration Rebuild(ITestOutput testOutput, string baseTestUrl = null, EnvironmentType? testEnvironmentType = null, ITestableWebPage baseTestPageType = null)
        {
            return TestConfiguration.Build(testOutput, baseTestUrl, testEnvironmentType, baseTestPageType);
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
            //I hate disabling the extensions but a popup window sometimes jumps out on tests in windows if you don't
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("chrome.switches", "--disable-extensions");

            return new ChromeDriver(options);
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
