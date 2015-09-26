using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Automation.Common.Selenium.Helpers;
using Automation.Common.Shared;
using Automation.Common.Shared.Exceptions;
using Automation.Common.Shared.Extensions;
using OpenQA.Selenium;

namespace Automation.Common.Selenium
{
    public class SeleniumWebObject : ISearchableWebObject
    {
        protected ISearchContext SearchContext;

        public IDictionary<string, IDictionary<string, string>> SubElements { get; set; } = new Dictionary<string, IDictionary<string, string>>(StringComparer.OrdinalIgnoreCase);

        public int DefaultActionTimeout { get; set; } = 60;

        public SeleniumWebObject(){}

        public SeleniumWebObject(ISearchContext searchContext)
        {
            SearchContext = searchContext;
        }

        /// <summary>
        /// Intended to be overridden so that we can enforce the selenium driver to focus on the window containing this element
        /// </summary>
        public virtual void EnsureElementFocus()
        {
            return;
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
            EnsureElementFocus();
            //If this element uses another element as a base then find the other element and search from there
            if (subElementProperties.ContainsKey("ParentElement"))
            {
                var elementProperties = new Dictionary<string, string>(subElementProperties, StringComparer.OrdinalIgnoreCase);
                var parentName = elementProperties["ParentElement"];
                elementProperties.Remove("ParentElement");

                return FindSubElement(GetElementProperties(parentName)).FindSubElement(elementProperties);
            }

            return SearchContext.FindSubElement(subElementProperties);
        }

        public ITestableWebElement FindSubElement(IDictionary<string, string> subElementProperties, int timeout)
        {
            //If this element uses another element as a base then find the other element and search from there
            if (subElementProperties.ContainsKey("ParentElement"))
            {
                var elementProperties = new Dictionary<string, string>(subElementProperties, StringComparer.OrdinalIgnoreCase);
                var parentName = elementProperties["ParentElement"];
                elementProperties.Remove("ParentElement");

                return FindSubElement(GetElementProperties(parentName), timeout).FindSubElement(elementProperties, timeout);
            }

            Stopwatch watch = new Stopwatch();
            watch.Start();
            ITestableWebElement element = null;

            ObjectNotFoundException lastException = null;
            //While the timeout has not elapsed attempt to find element
            while ((watch.ElapsedMilliseconds / 1000) < timeout)
            {
                try
                {
                    element = FindSubElement(subElementProperties);
                }
                catch (ObjectNotFoundException e)
                {
                    lastException = e;
                    //If timout is not elapsed then keep looking, otherwise throw the exception back up
                    if ((watch.ElapsedMilliseconds / 1000) < timeout)
                    {
                        Thread.Sleep(200);
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
            if (element == null)
            {
                throw lastException ?? new ObjectNotFoundException($"Failed to find element with properties {subElementProperties}");
            }
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
            EnsureElementFocus();
            //If this element uses another element as a base then find the other element and search from there
            if (subElementProperties.ContainsKey("ParentElement"))
            {
                var elementProperties = new Dictionary<string, string>(subElementProperties, StringComparer.OrdinalIgnoreCase);
                var parentName = elementProperties["ParentElement"];
                elementProperties.Remove("ParentElement");
                return FindSubElement(GetElementProperties(parentName)).FindSubElements(elementProperties);
            }
            return SearchContext.FindSubElements(subElementProperties);
        }

        public IList<ITestableWebElement> FindSubElements(IDictionary<string, string> subElementProperties, int timeout)
        {
            EnsureElementFocus();
            //If this element uses another element as a base then find the other element and search from there
            if (subElementProperties.ContainsKey("ParentElement"))
            {
                var elementProperties = new Dictionary<string, string>(subElementProperties, StringComparer.OrdinalIgnoreCase);
                var parentName = elementProperties["ParentElement"];
                elementProperties.Remove("ParentElement");
                return FindSubElement(GetElementProperties(parentName)).FindSubElements(elementProperties);
            }

            Stopwatch watch = new Stopwatch();
            watch.Start();
            IList<ITestableWebElement> elements = new List<ITestableWebElement>();

            //While the timeout has not elapsed attempt to find element
            while ((watch.ElapsedMilliseconds / 1000) < timeout)
            {
                elements = SearchContext.FindSubElements(subElementProperties);
                if (elements.Any())
                {
                    break;
                }
                Thread.Sleep(200);
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
            elementProperties = new Dictionary<string, string>(elementProperties, StringComparer.OrdinalIgnoreCase);
            SubElements.Add(name, elementProperties);
        }
    }
}
