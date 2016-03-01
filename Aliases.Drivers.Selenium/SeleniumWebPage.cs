using System;
using System.Diagnostics;
using System.Threading;
using Aliases.Common.Shared;
using Aliases.Common.Shared.Exceptions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Aliases.Drivers.Selenium
{
    /// <summary>
    /// This does not extent Selenium web element because according to Selenium the browser is not really an element, 
    /// actions agains this element need to get the root element first
    /// </summary>
    public class SeleniumWebPage : SeleniumWebObject, ITestableWebPage
    {
        private string WindowHandle { get; set; }

        public IWebDriver Driver { get; set; }

        public SeleniumWebPage(IWebDriver driver) : base(driver)
        {
            this.Driver = driver;
            this.SearchContext = driver;

            this.WindowHandle = driver.CurrentWindowHandle;
        }

        /// <summary>
        /// Returns a new page, used when creating new pages while reusing the same driver
        /// </summary>
        /// <returns></returns>
        public ITestableWebPage AsNew()
        {
            return new SeleniumWebPage(Driver);
        }

        public void EnsureFocus()
        {
            Driver.SwitchTo().Window(WindowHandle);
        }

        public override void EnsureElementFocus()
        {
            EnsureFocus();
            base.EnsureElementFocus();
        }

        public void SetActiveWindow(string windowUrlContains, int timeout)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while ((watch.ElapsedMilliseconds / 1000) < timeout)
            {
                foreach (var handle in Driver.WindowHandles)
                {
                    Driver.SwitchTo().Window(handle);
                    if (Driver.Url.Contains(windowUrlContains))
                    {
                        WindowHandle = handle;
                        return;
                    }
                }
                Driver.SwitchTo().Window(WindowHandle);
                Thread.Sleep(200);
            }
            watch.Stop();

            throw new ObjectNotFoundException($"Failed to find window that contains url {windowUrlContains}");
        }

        public void Open(Uri uri)
        {
            EnsureFocus();
            this.Driver.Navigate().GoToUrl(uri);
        }

        public void Open(string url)
        {
            EnsureFocus();
            this.Driver.Navigate().GoToUrl(url);
        }
        public void Close()
        {
            EnsureFocus();
            this.Driver.Close();
        }

        public void Maximize()
        {
            EnsureFocus();
            this.Driver.Manage().Window.Maximize();
        }
        
        public string GetCurrentUrl()
        {
            EnsureFocus();
            return Driver.Url;
        }

        public string GetScreenshot()
        {
            EnsureFocus();
            try
            {
                Screenshot ss = ((ITakesScreenshot)Driver).GetScreenshot();
                return ss.AsBase64EncodedString;
            }
            catch (UnhandledAlertException)
            {
                Driver.SwitchTo().Alert();
                Screenshot ss = ((ITakesScreenshot)Driver).GetScreenshot();
                return ss.AsBase64EncodedString;
            }
        }

        public void Type(string text)
        {
            EnsureFocus();
            Actions action = new Actions(this.Driver);
            action.SendKeys(text);
            action.Perform();
        }

        public void Click()
        {
            EnsureFocus();
            Actions action = new Actions(this.Driver);
            action.Click();
            action.Perform();
        }

        public bool WaitForAppear(int timeout)
        {
            return GetRootElement().WaitForAppear(timeout);
        }

        public bool WaitForDisappear(int timeout)
        {
            return GetRootElement().WaitForDisappear(timeout);
        }

        public bool IsDisplayed()
        {
            return GetRootElement().IsDisplayed();
        }

        public void WaitForAttributeState(string attributeName, Func<string, bool> condition, int timeout)
        {
            GetRootElement().WaitForAttributeState(attributeName, condition, timeout);
        }

        public string Attribute(string attributeName)
        {
            return GetRootElement().Attribute(attributeName);
        }

        public string TagName()
        {
            return "html";
        }

        public string InnerHtml()
        {
            return Attribute("outerHTML");
        }

        public SeleniumWebElement GetRootElement()
        {
            EnsureFocus();
            return new SeleniumWebElement(Driver.FindElement(By.TagName("html")));
        }

        public void Dispose()
        {
            EnsureFocus();
            this.Driver.Quit();
            this.Driver.Dispose();
        }

        public void BrowserBack()
        {
            EnsureFocus();
            this.Driver.Navigate().Back();
        }

        public IDialog Dialog()
        {
            return new SeleniumDialog(Driver);
        }
        
    }
}
