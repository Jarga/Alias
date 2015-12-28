using System;

namespace Alias.Common.Shared
{
    public interface ITestableWebElement : ISearchableWebObject
    {
        void Clear();
        void Type(string text);
        void Type(ITestableWebElement element, string text);
        void Click();
        void Click(ITestableWebElement element);
        bool Exists(string targetSubElement, int timeout);
        bool Exists(Types.Alias alias, int timeToLook);
        void Select(string item, bool isValue = false);
        void Select(ITestableWebElement element, string item, bool isValue = false);

        bool WaitForAppear(int timeout);
        bool WaitForAppear(string targetSubElement, int timeout);
        bool WaitForAppear(Types.Alias alias, int timeout);
        bool WaitForDisappear(int timeout);
        bool WaitForDisappear(string targetSubElement, int timeout);
        bool WaitForDisappear(Types.Alias alias, int timeout);
        bool IsDisplayed();
        bool IsSelected();
        void SetChecked(bool value);
        void SetChecked(ITestableWebElement element, bool value);
        void WaitForAttributeState(string attributeName, Func<string, bool> condition, int timeout);
        void WaitForAttributeState(string targetSubElement, string attributeName, Func<string, bool> condition, int timeout);
        void WaitForAttributeState(Types.Alias alias, string attributeName, Func<string, bool> condition, int timeout);

        string GetAttribute(string attributeName);
        string GetTagName();
        string InnerHtml();
        string GetText();
        string GetCssValue(string propertyName);
        ITestableWebElement Parent(int levels = 1);
    }
}
