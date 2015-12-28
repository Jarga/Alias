using System;

namespace Automation.Common.Shared
{
    public interface ITestableWebPage : ITestableWebElement, IDisposable
    {
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
    }
}
