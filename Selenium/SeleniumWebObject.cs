using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using OpenQA.Selenium;
using TestAutomation.Selenium.Helpers;
using TestAutomation.Shared;
using TestAutomation.Shared.Exceptions;
using TestAutomation.Shared.Extensions;

namespace TestAutomation.Selenium
{
    public class SeleniumWebObject : ISearchableWebObject
    {
        private IDictionary<string, IDictionary<string, string>> _subElements = new Dictionary<string, IDictionary<string, string>>(StringComparer.OrdinalIgnoreCase);

        protected ISearchContext _searchContext;
        private int _defaultActionTimeout = 60;

        public IDictionary<string, IDictionary<string, string>> SubElements
        {
            get { return _subElements; }
            set { _subElements = value; }
        }

        public int DefaultActionTimeout
        {
            get { return _defaultActionTimeout; }
            set { _defaultActionTimeout = value; }
        }

        public SeleniumWebObject(){}

        public SeleniumWebObject(ISearchContext searchContext)
        {
            _searchContext = searchContext;
        }

        public IDictionary<string, string> GetElementProperties(string targetElement)
        {
            if (!SubElements.ContainsKey(targetElement))
            {
                throw new ElementNotRegisteredException("Invalid Element Name");
            }
            return SubElements[targetElement];
        }

        public ITestableWebElement FindSubElement(string targetSubElement)
        {
            return FindSubElement(GetElementProperties(targetSubElement), this.DefaultActionTimeout);
        }

        public ITestableWebElement FindSubElement(string targetSubElement, int timeout)
        {
            return FindSubElement(GetElementProperties(targetSubElement), timeout);
        }

        public ITestableWebElement FindSubElement(IDictionary<string, string> subElementProperties)
        {
            //If this element uses another element as a base then find the other element and search from there
            if (subElementProperties.ContainsKey("ParentElement"))
            {
                var elementProperties = new Dictionary<string, string>(subElementProperties);
                var parentName = elementProperties["ParentElement"];
                elementProperties.Remove("ParentElement");

                return FindSubElement(GetElementProperties(parentName)).FindSubElement(elementProperties);
            }

            return _searchContext.FindSubElement(subElementProperties);
        }

        public ITestableWebElement FindSubElement(IDictionary<string, string> subElementProperties, int timeout)
        {
            //If this element uses another element as a base then find the other element and search from there
            if (subElementProperties.ContainsKey("ParentElement"))
            {
                var elementProperties = new Dictionary<string, string>(subElementProperties);
                var parentName = elementProperties["ParentElement"];
                elementProperties.Remove("ParentElement");

                return FindSubElement(GetElementProperties(parentName), timeout).FindSubElement(elementProperties, timeout);
            }

            Stopwatch watch = new Stopwatch();
            watch.Start();
            ITestableWebElement element = null;

            //While the timeout has not elapsed attempt to find element
            while ((watch.ElapsedMilliseconds / 1000) < timeout)
            {
                try
                {
                    element = FindSubElement(subElementProperties);
                }
                catch (ObjectNotFoundException)
                {
                    //If timout is not elapsed then keep looking, otherwise throw the exception back up
                    if ((watch.ElapsedMilliseconds / 1000) < timeout)
                    {
                        continue;
                    }
                    else
                    {
                        throw;
                    }
                }
                catch
                {
                    //Any other exceptions should be preserved
                    watch.Stop();
                    throw;
                }
                break;
            }
            watch.Stop();
            return element;
        }

        public IList<ITestableWebElement> FindSubElements(string targetSubElement)
        {
            return FindSubElements(GetElementProperties(targetSubElement));
        }

        public IList<ITestableWebElement> FindSubElements(string targetSubElement, int timeout)
        {
            return FindSubElements(GetElementProperties(targetSubElement), timeout);
        }

        public IList<ITestableWebElement> FindSubElements(IDictionary<string, string> subElementProperties)
        {
            //If this element uses another element as a base then find the other element and search from there
            if (subElementProperties.ContainsKey("ParentElement"))
            {
                var parentName = subElementProperties["ParentElement"];
                subElementProperties.Remove("ParentElement");
                return FindSubElement(GetElementProperties(parentName)).FindSubElements(subElementProperties);
            }
            return _searchContext.FindSubElements(subElementProperties);
        }

        public IList<ITestableWebElement> FindSubElements(IDictionary<string, string> subElementProperties, int timeout)
        {
            //If this element uses another element as a base then find the other element and search from there
            if (subElementProperties.ContainsKey("ParentElement"))
            {
                var parentName = subElementProperties["ParentElement"];
                subElementProperties.Remove("ParentElement");
                return FindSubElement(GetElementProperties(parentName)).FindSubElements(subElementProperties);
            }

            Stopwatch watch = new Stopwatch();
            watch.Start();
            IList<ITestableWebElement> elements = new List<ITestableWebElement>();

            //While the timeout has not elapsed attempt to find element
            while ((watch.ElapsedMilliseconds / 1000) < timeout && !elements.Any())
            {
                elements = _searchContext.FindSubElements(subElementProperties);
            }

            watch.Stop();

            return elements;
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
            RegisterSubElement(name, properties.ToArray());
        }

        public void RegisterSubElement(string name, params string[] elementProperties)
        {
            SubElements.AddElementDefinition(name, elementProperties);
        }

        public void RegisterSubElement(string name, IDictionary<string, string> elementProperties)
        {
            SubElements.Add(name, elementProperties);
        }
    }
}
