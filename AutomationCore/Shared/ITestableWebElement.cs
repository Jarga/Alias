using System;

namespace AutomationCore.Shared
{
    public interface ITestableWebElement : ISearchableWebObject
    {
        void Clear();
        void Type(string text);
        void Type(ITestableWebElement element, string text);
        void Click();
        void Click(ITestableWebElement element);
        void Select(string item, bool isValue = false);
        void Select(ITestableWebElement element, string item, bool isValue = false);

        bool WaitForAppear(string targetSubElement, int timeout);
        bool WaitForDisappear(string targetSubElement, int timeout);
        bool IsDisplayed();
        bool IsSelected();
        void SetChecked(bool value);
        void SetChecked(ITestableWebElement element, bool value);
        void WaitForAttributeState(string targetSubElement, string attributeName, Func<string, bool> condition, int timeout);

        string GetAttribute(string attributeName);
        string GetTagName();
        string InnerHtml();
        ITestableWebElement Parent(int levels = 1);
    }
}
