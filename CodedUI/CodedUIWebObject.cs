using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using TestAutomation.Shared;
using TestAutomation.CodedUI.Helpers;


namespace TestAutomation.CodedUI
{
    public abstract class CodedUIWebObject : ISearchableWebObject
    {
        protected UITestControl _baseObject;

        public int DefaultActionTimeout
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
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

        public ITestableWebElement FindSubElement(string targetSubElement)
        {
            throw new NotImplementedException();
        }

        public ITestableWebElement FindSubElement(string targetSubElement, int timeout)
        {
            throw new NotImplementedException();
        }

        public ITestableWebElement FindSubElement(IDictionary<string, string> subElementProperties)
        {
            return new CodedUIWebElement(_baseObject.Find(subElementProperties));
        }

        public ITestableWebElement FindSubElement(IDictionary<string, string> subElementProperties, int timeout)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            UITestControl element = _baseObject.Find(subElementProperties);

            while ((watch.ElapsedMilliseconds / 1000) < timeout && !element.WaitForControlReady(100))
            {
                if ((watch.ElapsedMilliseconds / 1000) < timeout)
                {
                    continue;
                }
                watch.Stop();
                throw new Exception(string.Format("Element does not exist on the page! Properties: {0}", subElementProperties));

            }
            watch.Stop();
            return new CodedUIWebElement(element);
        }

        public IList<ITestableWebElement> FindSubElements(string targetSubElement)
        {
            throw new NotImplementedException();
        }

        public IList<ITestableWebElement> FindSubElements(string targetSubElement, int timeout)
        {
            throw new NotImplementedException();
        }

        public IList<ITestableWebElement> FindSubElements(IDictionary<string, string> subElementProperties)
        {
            List<UITestControl> foundElements = _baseObject.FindAll(subElementProperties).ToList();
            return foundElements.Any() ? foundElements.Select(element => (ITestableWebElement) new CodedUIWebElement(element)).ToList() : new List<ITestableWebElement>();
        }

        public IList<ITestableWebElement> FindSubElements(IDictionary<string, string> subElementProperties, int timeout)
        {
            throw new NotImplementedException();
        }

        public void RegisterSubElement(string name, dynamic elementProperties)
        {
            throw new NotImplementedException();
        }

        public void RegisterSubElement(string name, params string[] elementProperties)
        {
            throw new NotImplementedException();
        }

        public void RegisterSubElement(string name, IDictionary<string, string> elementProperties)
        {
            throw new NotImplementedException();
        }
    }
}
