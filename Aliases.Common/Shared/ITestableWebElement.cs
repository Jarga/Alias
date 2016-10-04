using System;

namespace Aliases.Common.Shared
{
    public interface ITestableWebElement : ISearchableWebObject
    {
        void Clear();
        void Type(string text);
        void Click();
        void Select(string item, bool isValue = false);
        void SetChecked(bool value);

        bool WaitForAppear(int timeout);
        bool WaitForDisappear(int timeout);
        bool IsDisplayed();
        bool IsSelected();
        bool IsEnabled();
        void WaitForAttributeState(string attributeName, Func<string, bool> condition, int timeout);
       
        string Attribute(string attributeName);
        string TagName();
        string InnerHtml();
        string Text();
        string CssValue(string propertyName);

        ITestableWebElement Parent(int levels = 1);
    }
}
