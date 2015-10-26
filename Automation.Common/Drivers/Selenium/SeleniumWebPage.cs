using System;
using System.Diagnostics;
using System.Threading;
using Automation.Common.Shared;
using Automation.Common.Shared.Exceptions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Automation.Common.Drivers.Selenium
{
    /// <summary>
    /// This does not extent Selenium web element because according to Selenium the browser is not really an element, 
    /// actions agains this element need to get the root element first
    /// </summary>
    public class SeleniumWebPage : SeleniumWebObject, ITestableWebPage
    {
        private string WindowHandle { get; set; }

        public IWebDriver Driver { get; set; }

        public SeleniumWebPage(IWebDriver driver)
        {
            this.Driver = driver;
            this.SearchContext = driver;

            this.WindowHandle = driver.CurrentWindowHandle;
        }

        /// <summary>
        /// Returns a new page with the SubElements empty, used when creating new pages while reusing the same driver
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
            Screenshot ss = ((ITakesScreenshot)Driver).GetScreenshot();
            return ss.AsBase64EncodedString;
        }

        public void Clear()
        {
            //Can't clear the browser
        }

        public void Type(string text)
        {
            EnsureFocus();
            Actions action = new Actions(this.Driver);
            action.SendKeys(text);
            action.Perform();
        }

        public void Type(ITestableWebElement element, string text)
        {
            element.Type(text);
        }

        public void Click()
        {
            EnsureFocus();
            Actions action = new Actions(this.Driver);
            action.Click();
            action.Perform();
        }

        public void Click(ITestableWebElement element)
        {
            element.Click();
        }

        public bool Exists(string targetSubElement, int timeout)
        {
            return GetRootElement().Exists(targetSubElement, timeout);
        }

        public void Select(string item, bool isValue = false)
        {
            //Cannot Perform Selection on Browser
        }

        public void Select(ITestableWebElement element, string item, bool isValue = false)
        {
            element.Select(item, isValue);
        }

        public bool WaitForAppear(int timeout)
        {
            return GetRootElement().WaitForAppear(timeout);
        }

        public bool WaitForAppear(string targetSubElement, int timeout)
        {
            return GetRootElement().WaitForAppear(targetSubElement, timeout);
        }

        public bool WaitForDisappear(int timeout)
        {
            return GetRootElement().WaitForDisappear(timeout);
        }

        public bool WaitForDisappear(string targetSubElement, int timeout)
        {
            return GetRootElement().WaitForDisappear(targetSubElement, timeout);
        }

        public void SetChecked(ITestableWebElement element, bool value)
        {
            element.SetChecked(value);
        }

        public void WaitForAttributeState(string attributeName, Func<string, bool> condition, int timeout)
        {
            GetRootElement().WaitForAttributeState(attributeName, condition, timeout);
        }

        public void WaitForAttributeState(string targetSubElement, string attributeName, Func<string, bool> condition, int timeout)
        {
            GetRootElement().WaitForAttributeState(targetSubElement, attributeName, condition, timeout);
        }

        public bool IsDisplayed()
        {
            return GetRootElement().IsDisplayed();
        }

        public bool IsSelected()
        {
            return false;
        }

        public void SetChecked(bool value)
        {
            return;
        }

        public string GetAttribute(string attributeName)
        {
            return GetRootElement().GetAttribute(attributeName);
        }

        public string GetTagName()
        {
            return "html";
        }

        public string InnerHtml()
        {
            return GetAttribute("outerHTML");
        }

        public string GetText()
        {
            return GetRootElement().GetText();
        }

        public ITestableWebElement Parent(int levels = 1)
        {
            return null; // There is no parent to the browser
        }

        public SeleniumWebElement GetRootElement()
        {
            EnsureFocus();
            //Set subelements to this browser's sub elements to prevent need to re-register
            return new SeleniumWebElement(Driver.FindElement(By.TagName("html")))
            {
                SubElements = SubElements
            };
        }

        public void Dispose()
        {
            EnsureFocus();
            this.Driver.Quit();
            this.Driver.Dispose();
        }

    }
}
