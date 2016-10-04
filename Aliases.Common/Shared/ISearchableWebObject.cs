using System;
using System.Collections.Generic;
using Aliases.Common.Shared.Types;

namespace Aliases.Common.Shared
{
    public interface ISearchableWebObject
    {
        int DefaultActionTimeout { get; set; }
        ITestableWebElement FindSubElement(Alias subElementProperties);
        ITestableWebElement FindSubElement(Alias subElementProperties, int timeout);
        IList<ITestableWebElement> FindSubElements(Alias subElementProperties);
        IList<ITestableWebElement> FindSubElements(Alias subElementProperties, int timeout);

       
        void Type(Alias alias, string text);
        void Click(Alias alias);
        bool Exists(Alias alias, int timeToLook);
        void Select(Alias alias, string item, bool isValue = false);
        void SetChecked(Alias alias, bool value);
        bool WaitForAppear(Alias alias, int timeout);
        bool WaitForDisappear(Alias alias, int timeout);
        bool IsDisplayed(Alias alias);
        bool IsSelected(Alias alias);
        bool IsEnabled(Alias alias);
        void WaitForAttributeState(Alias alias, string attributeName, Func<string, bool> condition, int timeout);
        string Attribute(Alias alias, string attributeName);
        string TagName(Alias alias);
        string InnerHtml(Alias alias);
        string Text(Alias alias);
        string CssValue(Alias alias, string propertyName);
        ITestableWebElement Parent(Alias alias, int levels = 1);
    }
}
