using System;
using Automation.Common.Shared;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Automation.Common.Selenium
{
    /// <summary>
    /// This does not extent Selenium web element because according to Selenium the browser is not really an element, 
    /// actions agains this element need to get the root element first
    /// </summary>
    public class SeleniumWebPage : SeleniumWebObject, ITestableWebPage
    {
        private IWebDriver _driver;
        
        public IWebDriver Driver
        {
            get { return _driver; }
            set { _driver = value; }
        }

        public SeleniumWebPage(IWebDriver driver)
        {
            this._driver = driver;
            this._searchContext = driver;
        }

        /// <summary>
        /// Returns a new page with the SubElements empty, used when creating new pages while reusing the same driver
        /// </summary>
        /// <returns></returns>
        public ITestableWebPage AsNew()
        {
            return new SeleniumWebPage(_driver);
        }

        public void Open(Uri uri)
        {
            this._driver.Navigate().GoToUrl(uri);
        }

        public void Open(string url)
        {
            this._driver.Navigate().GoToUrl(url);
        }

        public void Close()
        {
            this._driver.Quit();
            this._driver.Dispose();
        }

        public void Maximize()
        {
            this._driver.Manage().Window.Maximize();
        }
        
        public string GetCurrentUrl()
        {
            return _driver.Url;
        }

        public string GetScreenshot()
        {
            Screenshot ss = ((ITakesScreenshot)_driver).GetScreenshot();
            return ss.AsBase64EncodedString;
        }

        public void Clear()
        {
            //Can't clear the browser
        }

        public void Type(string text)
        {
            Actions action = new Actions(this._driver);
            action.SendKeys(text);
            action.Perform();
        }

        public void Type(ITestableWebElement element, string text)
        {
            element.Type(text);
        }

        public void Click()
        {
            Actions action = new Actions(this._driver);
            action.Click();
            action.Perform();
        }

        public void Click(ITestableWebElement element)
        {
            element.Click();
        }

        public void Select(string item, bool isValue = false)
        {
            //Cannot Perform Selection on Browser
        }

        public void Select(ITestableWebElement element, string item, bool isValue = false)
        {
            element.Select(item, isValue);
        }

        public bool WaitForAppear(string targetSubElement, int timeout)
        {
            return GetRootElement().WaitForAppear(targetSubElement, timeout);
        }

        public bool WaitForDisappear(string targetSubElement, int timeout)
        {
            return GetRootElement().WaitForDisappear(targetSubElement, timeout);
        }

        public void SetChecked(ITestableWebElement element, bool value)
        {
            element.SetChecked(value);
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

        public ITestableWebElement Parent(int levels = 1)
        {
            return null; // There is no parent to the browser
        }

        public SeleniumWebElement GetRootElement()
        {
            //Set subelements to this browser's sub elements to prevent need to re-register
            return new SeleniumWebElement(_driver.FindElement(By.TagName("html")))
            {
                SubElements = SubElements
            };
        }

    }
}
