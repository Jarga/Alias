using System;

namespace Aliases.Common.Shared
{
    public interface ITestableWebPage : ISearchableWebObject, IDisposable
    {
        //AsNew() allows a hook to reset any residual state that may have been set since the page has been created.
        ITestableWebPage AsNew();
        void EnsureFocus();
        void SetActiveWindow(string windowUrlContains, int timeout);
        void Open(Uri uri);
        void Open(string url);
        void Close();
        void Maximize();
        string GetCurrentUrl();
        string GetScreenshot();
        void BrowserBack();
        IDialog Dialog();

        void Type(string text);
        void Click();
        bool WaitForAppear(int timeout);
        bool WaitForDisappear(int timeout);
        bool IsDisplayed();
        void WaitForAttributeState(string attributeName, Func<string, bool> condition, int timeout);
        string Attribute(string attributeName);
        string TagName();
        string InnerHtml();
    }
}
