using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using OpenQA.Selenium;
using TestAutomation.Selenium.Helpers;
using TestAutomation.Shared;
using TestAutomation.Shared.Exceptions;

namespace TestAutomation.Selenium
{
    public abstract class SeleniumWebObject : ISearchableWebObject
    {
        private IDictionary<string, IDictionary<string, string>> _subElements = new Dictionary<string, IDictionary<string, string>>();

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
                var parentName = subElementProperties["ParentElement"];
                subElementProperties.Remove("ParentElement");
                return FindSubElement(GetElementProperties(parentName)).FindSubElement(subElementProperties);
            }

            return _searchContext.FindSubElement(subElementProperties);
        }

        public ITestableWebElement FindSubElement(IDictionary<string, string> subElementProperties, int timeout)
        {
            //If this element uses another element as a base then find the other element and search from there
            if (subElementProperties.ContainsKey("ParentElement"))
            {
                var parentName = subElementProperties["ParentElement"];
                subElementProperties.Remove("ParentElement");
                return FindSubElement(GetElementProperties(parentName), timeout).FindSubElement(subElementProperties, timeout);
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
    }
}
