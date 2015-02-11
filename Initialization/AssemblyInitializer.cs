using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
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
            GlobalTestSettings.Environment = Environments.QA;
            GlobalTestSettings.BaseTestPageType = new SeleniumWebPage(new InternetExplorerDriver( new InternetExplorerOptions(){ IgnoreZoomLevel = true }));
            //GlobalTestSettings.BaseTestPageType = new SeleniumWebPage(new ChromeDriver());
            //GlobalTestSettings.BaseTestPageType = new SeleniumWebPage(new FirefoxDriver());
            //GlobalTestSettings.BaseTestPageType = new CodedUIWebPage();
        }
    }
}
