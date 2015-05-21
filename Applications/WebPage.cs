using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using TestAutomation.Shared;
using TestAutomation.Shared.Extensions;

namespace TestAutomation.Applications
{
    /// <summary>
    /// This object acts as a proxy for the actual implementation object, it is intended to hide the specific framework implementation
    /// </summary>
    public class WebPage : ITestableWebPage
    {
        private ITestableWebPage _baseObject;
        
        public ITestableWebPage BaseObject
        {
            get { return _baseObject; }
            set { _baseObject = value; }
        }

        public WebPage(ITestableWebPage baseObject)
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

            return ctor.Invoke(new object[] { BaseObject }) as T;
        }

        public void Clear()
        {
            _baseObject.Clear();
        }

        public void Type(string text)
        {
            _baseObject.Type(text);
        }

        public void Type(ITestableWebElement element, string text)
        {
            _baseObject.Type(element, text);
        }

        public void Type(string targetSubElement, string text)
        {
            Type(FindSubElement(targetSubElement), text);
        }

        public void Click()
        {
            _baseObject.Click();
        }

        public void Click(ITestableWebElement element)
        {
            _baseObject.Click(element);
        }

        public bool WaitForAppear(string targetSubElement, int timeout)
        {
            return _baseObject.WaitForAppear(targetSubElement, timeout);
        }

        public bool WaitForDisappear(string targetSubElement, int timeout)
        {
            return _baseObject.WaitForDisappear(targetSubElement, timeout);
        }

        public bool IsDisplayed()
        {
            return _baseObject.IsDisplayed();
        }

        public void WaitForAttributeState(string targetSubElement, string attributeName, Func<string, bool> condition, int timeout)
        {
            _baseObject.WaitForAttributeState(targetSubElement, attributeName, condition, timeout);
        }

        public string GetAttribute(string attributeName)
        {
            return _baseObject.GetAttribute(attributeName);
        }

        public string InnerHtml()
        {
            return _baseObject.InnerHtml();
        }

        public ITestableWebElement Parent(int levels = 1)
        {
            return _baseObject.Parent(levels);
        }
        
        public void Click(string targetSubElement)
        {
            Click(FindSubElement(targetSubElement));
        }

        public bool Exists(string targetSubElement)
        {
            return Exists(targetSubElement, this.DefaultActionTimeout);
        }

        public bool WaitFor(string targetSubElement, int timeToLook = 60)
        {
            return Exists(targetSubElement, timeToLook);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetSubElement">Element to look for</param>
        /// <param name="timeToLook">Seconds to wait for it to appear</param>
        /// <returns></returns>
        public bool Exists(string targetSubElement, int timeToLook)
        {

            Stopwatch watch = new Stopwatch();
            watch.Start();

            while ((watch.ElapsedMilliseconds / 1000) < timeToLook)
            {
                IList<ITestableWebElement> elementsFound = FindSubElements(targetSubElement);
                if (elementsFound.Any())
                {
                    watch.Stop();
                    return true;
                }
            }
            watch.Stop();
            return false;
        }
        
        public int DefaultActionTimeout
        {
            get { return _baseObject.DefaultActionTimeout; }
            set { _baseObject.DefaultActionTimeout = value; }
        }

        public IDictionary<string, IDictionary<string, string>> SubElements
        {
            get { return _baseObject.SubElements; }
            set { _baseObject.SubElements = value; }
        }

        public IDictionary<string, string> GetElementProperties(string targetElement)
        {
            return _baseObject.GetElementProperties(targetElement);
        }

        public ITestableWebElement FindSubElement(string targetSubElement)
        {
            return _baseObject.FindSubElement(targetSubElement);
        }

        public ITestableWebElement FindSubElement(string targetSubElement, int timeout)
        {
            return _baseObject.FindSubElement(targetSubElement, timeout);
        }

        public ITestableWebElement FindSubElement(IDictionary<string, string> subElementProperties)
        {
            return _baseObject.FindSubElement(subElementProperties);
        }

        public ITestableWebElement FindSubElement(IDictionary<string, string> subElementProperties, int timeout)
        {
            return _baseObject.FindSubElement(subElementProperties, timeout);
        }

        public IList<ITestableWebElement> FindSubElements(string targetSubElement)
        {
            return _baseObject.FindSubElements(targetSubElement);
        }

        public IList<ITestableWebElement> FindSubElements(string targetSubElement, int timeout)
        {
            return _baseObject.FindSubElements(targetSubElement, timeout);
        }

        public IList<ITestableWebElement> FindSubElements(IDictionary<string, string> subElementProperties)
        {
            return _baseObject.FindSubElements(subElementProperties);
        }

        public IList<ITestableWebElement> FindSubElements(IDictionary<string, string> subElementProperties, int timeout)
        {
            return _baseObject.FindSubElements(subElementProperties, timeout);
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

        public void ResetZoomLevel()
        {
            _baseObject.ResetZoomLevel();
        }

        public string GetCurrentUrl()
        {
            return _baseObject.GetCurrentUrl();
        }

        public void RegisterSubElement(string name, dynamic elementProperties)
        {
            _baseObject.RegisterSubElement(name, elementProperties as object);
        }

        public void RegisterSubElement(string name, params string[] elementProperties)
        {
            _baseObject.RegisterSubElement(name, elementProperties);
        }

        public void RegisterSubElement(string name, IDictionary<string, string> elementProperties)
        {
            _baseObject.RegisterSubElement(name, elementProperties);
        }
    }
}
