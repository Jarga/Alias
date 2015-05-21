using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomation.Shared
{
    public interface ITestableWebElement : ISearchableWebObject
    {
        void Clear();
        void Type(string text);
        void Type(ITestableWebElement element, string text);
        void Click();
        void Click(ITestableWebElement element);

        bool WaitForAppear(string targetSubElement, int timeout);
        bool WaitForDisappear(string targetSubElement, int timeout);
        bool IsDisplayed();
        void WaitForAttributeState(string targetSubElement, string attributeName, Func<string, bool> condition, int timeout);

        string GetAttribute(string attributeName);
        string InnerHtml();
        ITestableWebElement Parent(int levels = 1);
    }
}
