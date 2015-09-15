using System;

namespace AutomationCore.Shared
{
    public interface ITestableWebPage : ITestableWebElement
    {
        ITestableWebPage AsNew();
        void Open(Uri uri);
        void Open(string url);
        void Close();
        void Maximize();
        string GetCurrentUrl();
        string GetScreenshot();
    }
}
