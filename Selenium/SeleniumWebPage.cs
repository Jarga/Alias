using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestAutomation.Shared;

namespace TestAutomation.Selenium
{
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

        public void ResetZoomLevel()
        {
            Type(Keys.Control + "0");
        }

        public string GetCurrentUrl()
        {
            return _driver.Url;
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

        public bool WaitForAppear(string targetSubElement, int timeout)
        {
            return GetRootElement().WaitForAppear(targetSubElement, timeout);
        }

        public bool WaitForDisappear(string targetSubElement, int timeout)
        {
            return GetRootElement().WaitForDisappear(targetSubElement, timeout);
        }

        public void WaitForAttributeState(string targetSubElement, string attributeName, Func<string, bool> condition, int timeout)
        {
            GetRootElement().WaitForAttributeState(targetSubElement, attributeName, condition, timeout);
        }

        public bool IsDisplayed()
        {
            return GetRootElement().IsDisplayed();
        }

        public string GetAttribute(string attributeName)
        {
            return GetRootElement().GetAttribute(attributeName);
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
