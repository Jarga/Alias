using OpenQA.Selenium;
using TestAutomation.Shared;

namespace TestAutomation.Selenium
{
    public class SeleniumWebElement : SeleniumWebObject, ITestableWebElement
    {
        public IWebElement _baseObject;

        public SeleniumWebElement(IWebElement baseObject)
        {
            this._baseObject = baseObject;
        }

        public void Type(string text)
        {
            _baseObject.SendKeys(text);
        }

        public void Type(ITestableWebElement element, string text)
        {
            element.Type(text);
        }

        public void Click()
        {
            _baseObject.Click();
        }

        public void Click(ITestableWebElement element)
        {
            element.Click();
        }

    }
}
