using System;
using Aliases.Common.Shared.Types;

namespace Aliases.Common.Shared
{
    //Clone() allows a hook to reset any residual state that may have been set since the page has been created.
    public interface ITestableWebPage : ISearchableWebObject, IDisposable, ICloneable
    {
        void EnsureFocus();
        void SetActiveWindow(string windowUrlContains, int timeout);
        void Open(Uri uri);
        void Open(string url);
        void Close();
        void Maximize();
        string GetCurrentUrl();
        string GetScreenshot();
        void BrowserBack();

        void Type(string text);
        void Click();
        bool WaitForAppear(int timeout);
        bool WaitForDisappear(int timeout);
        bool IsDisplayed();
        void WaitForAttributeState(string attributeName, Func<string, bool> condition, int timeout);
        string Attribute(string attributeName);
        string TagName();
        string InnerHtml();

        IDialog Dialog();
        ITestableWebPage Frame(Alias alias);
        void Hover(Alias alias);
    }
}
