using System;
using System.Collections.Generic;
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

        public IDictionary<string, IDictionary<string, string>> SubElements
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IDictionary<string, string> GetElementProperties(string targetElement)
        {
            throw new NotImplementedException();
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

        public void ResetZoomLevel()
        {
            throw new NotImplementedException();
        }

        public string GetCurrentUrl()
        {
            return _browser.Uri.AbsoluteUri;
        }

        public void Clear()
        {
            throw new NotImplementedException();
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

        public string InnerHtml()
        {
            throw new NotImplementedException();
        }

        public ITestableWebElement Parent(int? levels = null)
        {
            throw new NotImplementedException();
        }
    }
}
