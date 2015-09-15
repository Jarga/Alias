using System;
using AutomationCore.Output;
using AutomationCore.Shared;

namespace AutomationCore
{
    /// <summary>
    /// This object acts as a proxy for the actual implementation object, it is intended to hide the specific framework implementation
    /// </summary>
    public class WebPage : WebElement, ITestableWebPage
    {
        private ITestableWebPage _baseObject;
        
        public new ITestableWebPage BaseObject
        {
            get { return _baseObject; }
            set { _baseObject = value; }
        }

        public WebPage(ITestableWebPage baseObject) : base(baseObject)
        {
            _baseObject = baseObject;
        }

        public T New<T>() where T : class, ITestableWebPage
        {
            var ctor = typeof(T).GetConstructor(new[] { typeof(ITestableWebPage) });

            if (ctor == null)
            {
                throw new Exception(string.Format("Unable to construct page for type {0}, no constructor exists.", typeof(T)));
            }

            return ctor.Invoke(new object[] { AsNew() }) as T;
        }

        public ITestableWebPage AsNew()
        {
            return _baseObject.AsNew();
        }

        public void Open(Uri uri)
        {
            _baseObject.Open(uri);
        }

        public void Open(string url)
        {
            _baseObject.Open(url);
        }

        public void Close()
        {
            _baseObject.Close();
        }

        public void Maximize()
        {
            _baseObject.Maximize();
        }

        public string GetCurrentUrl()
        {
            return _baseObject.GetCurrentUrl();
        }

        public string GetScreenshot()
        {
            return _baseObject.GetScreenshot();
        }
    }
}
