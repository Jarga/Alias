using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using TestAutomation.Shared;
using TestAutomation.Shared.Exceptions;
using TestAutomation.Shared.Extensions;

namespace TestAutomation.Applications
{
    public class WebPage : ITestableWebPage
    {
        private IDictionary<string, IDictionary<string, string>> _subElements = new Dictionary<string, IDictionary<string, string>>();
        
        private ITestableWebPage _baseObject;

        private int _defaultActionTimeout = 60;

        public int DefaultActionTimeout
        {
            get { return _defaultActionTimeout; }
            set { _defaultActionTimeout = value; }
        }

        public ITestableWebPage BaseObject
        {
            get { return _baseObject; }
            set { _baseObject = value; }
        }

        public WebPage(ITestableWebPage baseObject)
        {
            _baseObject = baseObject;
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

        public void Click(string targetSubElement)
        {
            Click(FindSubElement(targetSubElement));
        }

        public bool Exists(string targetSubElement)
        {
            return Exists(targetSubElement, this._defaultActionTimeout);
        }

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

        public ITestableWebElement FindSubElement(IDictionary<string, string> subElementProperties)
        {
            return FindSubElement(subElementProperties, this._defaultActionTimeout);
        }

        public ITestableWebElement FindSubElement(IDictionary<string, string> subElementProperties, int timeout)
        {
            return _baseObject.FindSubElement(subElementProperties, timeout);
        }

        public IList<ITestableWebElement> FindSubElements(IDictionary<string, string> subElementProperties)
        {
            return _baseObject.FindSubElements(subElementProperties);
        }

        public ITestableWebElement FindSubElement(string targetSubElement)
        {

            if (!_subElements.ContainsKey(targetSubElement))
            {
                throw new ElementNotRegisteredException("Invalid Element Name");
            }
            var properties = _subElements[targetSubElement];

            return FindSubElement(properties, this._defaultActionTimeout);
        }

        public ITestableWebElement FindSubElement(string targetSubElement, int timeout)
        {

            if (!_subElements.ContainsKey(targetSubElement))
            {
                throw new ElementNotRegisteredException("Invalid Element Name");
            }
            var properties = _subElements[targetSubElement];

            return FindSubElement(properties, timeout);
        }

        public IList<ITestableWebElement> FindSubElements(string targetSubElement)
        {

            if (!_subElements.ContainsKey(targetSubElement))
            {
                throw new ElementNotRegisteredException("Invalid Element Name");
            }
            var properties = _subElements[targetSubElement];

            return FindSubElements(properties);
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

        public void RegisterSubElement(string name, dynamic elementProperties)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(elementProperties);
            List<string> properties = new List<string>();
            foreach (PropertyDescriptor prop in props)
            {
                var value = prop.GetValue(elementProperties);
                if (value != null)
                {
                    string val = value.ToString();
                    properties.Add(prop.Name);
                    properties.Add(val);
                }
            }
            _subElements.AddDictionaryItems(name, properties.ToArray());
        }

        public void RegisterSubElement(string name, params string[] elementProperties)
        {
            _subElements.AddDictionaryItems(name, elementProperties);
        }
    }
}
