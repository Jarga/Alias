using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.IE;
using TestAutomation.CodedUI;
using TestAutomation.Selenium;
using TestAutomation.Shared.Enumerations;

namespace TestAutomation.Initialization
{
    [TestClass]
    public class AssemblyInitializer
    {
        [AssemblyInitialize()]
        public static void Build_Global_Test_Settings(TestContext context)
        {
            GlobalTestSettings.Environment = Environments.DEV;
            //GlobalTestSettings.BaseTestPageType = new SeleniumWebPage(new InternetExplorerDriver());
            //GlobalTestSettings.BaseTestPageType = new SeleniumWebPage(new ChromeDriver());
            //GlobalTestSettings.BaseTestPageType = new SeleniumWebPage(new FirefoxDriver());
            GlobalTestSettings.BaseTestPageType = new CodedUIWebPage();
        }
    }
}
