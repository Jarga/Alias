using System;
using Microsoft.VisualStudio.TestTools.UITesting;
using TestAutomation.Shared;

namespace TestAutomation.CodedUI
{
    class CodedUIWebPage : CodedUIWebObject, ITestableWebPage
    {
        private BrowserWindow _browser;
        
        public BrowserWindow Browser
        {
            get { return _browser; }
            set { _browser = value; }
        }
        
        public void Open(Uri uri)
        {
            this._browser = BrowserWindow.Launch(uri.AbsoluteUri, "-noframemerging");
            this._baseObject = _browser;
        }

        public void Open(string url)
        {
            this._browser = BrowserWindow.Launch(url, "-noframemerging");
            this._baseObject = _browser;
        }

        public void Close()
        {
            this._browser.Close();
            this._browser.Dispose();
        }

        public void Maximize()
        {
            this._browser.Maximized = true;
        }

        public string GetCurrentUrl()
        {
            return _browser.Uri.AbsoluteUri;
        }

        public void Type(string text)
        {
            Keyboard.SendKeys(_browser, text);
        }

        public void Type(ITestableWebElement element, string text)
        {
            element.Type(text);
        }

        public void Click()
        {
            Mouse.Click(_browser);
        }

        public void Click(ITestableWebElement element)
        {
            element.Click();
        }
        
    }
}
