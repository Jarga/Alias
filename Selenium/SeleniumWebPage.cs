using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestAutomation.Shared;
using TestAutomation.Shared.Exceptions;

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

        public string InnerHtml()
        {
            return _driver.FindElement(By.TagName("html")).GetAttribute("outerHTML");
        }

        public ITestableWebElement Parent(int? levels = null)
        {
            return null; // There is no parent to the browser
        }

    }
}
