using System;
using Aliases.Common.Shared;

namespace Aliases.Common
{
    public class WebElement : WebObject, ITestableWebElement
    {
        public new ITestableWebElement BaseObject { get; set; }

        public WebElement(ITestableWebElement baseObject) : base(baseObject)
        {
            BaseObject = baseObject;
        }

        public void Clear()
        {
            BaseObject.Clear();
        }

        public void Type(string text)
        {
            BaseObject.Type(text);
        }

        public void Click()
        {
            BaseObject.Click();
        }

        public void Select(string item, bool isValue = false)
        {
            BaseObject.Select(item, isValue);
        }

        public bool WaitForAppear()
        {
            return BaseObject.WaitForAppear(DefaultActionTimeout);
        }

        public bool WaitForAppear(int timeout)
        {
            return BaseObject.WaitForAppear(timeout);
        }

        public bool WaitForDisappear()
        {
            return BaseObject.WaitForDisappear(DefaultActionTimeout);
        }

        public bool WaitForDisappear(int timeout)
        {
            return BaseObject.WaitForDisappear(timeout);
        }

        public bool IsDisplayed()
        {
            return BaseObject.IsDisplayed();
        }

        public bool IsSelected()
        {
            return BaseObject.IsSelected();
        }

        public void SetChecked(bool value)
        {
            BaseObject.SetChecked(value);
        }

        public void WaitForAttributeState(string attributeName, Func<string, bool> condition)
        {
            BaseObject.WaitForAttributeState(attributeName, condition, DefaultActionTimeout);
        }

        public void WaitForAttributeState(string attributeName, Func<string, bool> condition, int timeout)
        {
            BaseObject.WaitForAttributeState(attributeName, condition, timeout);
        }

        public string Attribute(string attributeName)
        {
            return BaseObject.Attribute(attributeName);
        }

        public string TagName()
        {
            return BaseObject.TagName();
        }

        public string InnerHtml()
        {
            return BaseObject.InnerHtml();
        }

        public string Text()
        {
            return BaseObject.Text();
        }

        public string CssValue(string propertyName)
        {
            return BaseObject.CssValue(propertyName);
        }

        public ITestableWebElement Parent(int levels = 1)
        {
            return BaseObject.Parent(levels);
        }
    }
}
