using System;
using AutomationCore.Shared;
using Microsoft.VisualStudio.TestTools.UITesting;

namespace AutomationCore.CodedUI
{
    public class CodedUIWebPage : CodedUIWebElement, ITestableWebPage
    {
        private BrowserWindow _browser;
        
        public BrowserWindow Browser
        {
            get { return _browser; }
            set { _browser = value; }
        }

        public CodedUIWebPage(BrowserWindow browser)
            : base(browser.CurrentDocumentWindow)
        {
            _browser = browser;
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

        public string GetScreenshot()
        {
            throw new NotImplementedException();
        }
    }
}
