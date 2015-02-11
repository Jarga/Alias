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
            this._searchContext = baseObject;
        }

        public void Clear()
        {
            _baseObject.Clear();
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

        public string InnerHtml()
        {
            return _baseObject.GetAttribute("innerHTML");
        }

        public ITestableWebElement Parent(int? levels = null)
        {
            var xpath = "..";

            if (levels.HasValue)
            {
                for (int i = 1; i < levels.Value; i++)
                {
                    xpath += "/..";
                }
            }

            return new SeleniumWebElement(_baseObject.FindElement(By.XPath(xpath)));
        }
    }
}
