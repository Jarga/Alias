using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Alias.Common.Drivers.Selenium.Helpers;
using Alias.Common.Shared;
using Alias.Common.Shared.Exceptions;
using Alias.Common.Shared.Extensions;
using OpenQA.Selenium;

namespace Alias.Common.Drivers.Selenium
{
    public class SeleniumWebObject : ISearchableWebObject
    {
        protected ISearchContext SearchContext;

        public IDictionary<string, Shared.Types.Alias> SubElements { get; set; } = new Dictionary<string, Shared.Types.Alias>(StringComparer.OrdinalIgnoreCase);

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

        public Shared.Types.Alias GetElementProperties(string targetElement)
        {
            if (!SubElements.ContainsKey(targetElement))
            {
                throw new ElementNotRegisteredException($"Invalid Element Name: {targetElement}");
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

        public ITestableWebElement FindSubElement(Shared.Types.Alias subElementProperties)
        {
            EnsureElementFocus();

            //If this element uses another element as a base then find the other element and search from there
            if (subElementProperties.RelativeTo != null)
            {
                return FindSubElement(subElementProperties.RelativeTo).FindSubElement(new Shared.Types.Alias() { Definition = subElementProperties.Definition });
            }

            if (subElementProperties.ParentElement != null)
            {
                return FindSubElement(GetElementProperties(subElementProperties.ParentElement)).FindSubElement(new Shared.Types.Alias() { Definition = subElementProperties.Definition });
            }

            return SearchContext.FindSubElement(subElementProperties.Definition);
        }

        public ITestableWebElement FindSubElement(Shared.Types.Alias subElementProperties, int timeout)
        {
            //If this element uses another element as a base then find the other element and search from there
            if (subElementProperties.RelativeTo != null)
            {
                return FindSubElement(subElementProperties.RelativeTo, timeout).FindSubElement(new Shared.Types.Alias() { Definition = subElementProperties.Definition }, timeout);
            }

            if (subElementProperties.ParentElement != null)
            {
                return FindSubElement(GetElementProperties(subElementProperties.ParentElement), timeout).FindSubElement(new Shared.Types.Alias() {Definition = subElementProperties.Definition }, timeout);
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

        public IList<ITestableWebElement> FindSubElements(Shared.Types.Alias subElementProperties)
        {
            EnsureElementFocus();

            //If this element uses another element as a base then find the other element and search from there
            if (subElementProperties.RelativeTo != null)
            {
                return FindSubElement(subElementProperties.RelativeTo).FindSubElements(new Shared.Types.Alias() { Definition = subElementProperties.Definition });
            }

            if (subElementProperties.ParentElement != null)
            {
                return FindSubElement(GetElementProperties(subElementProperties.ParentElement)).FindSubElements(new Shared.Types.Alias() { Definition = subElementProperties.Definition });
            }

            return SearchContext.FindSubElements(subElementProperties.Definition);
        }

        public IList<ITestableWebElement> FindSubElements(Shared.Types.Alias subElementProperties, int timeout)
        {
            //If this element uses another element as a base then find the other element and search from there
            if (subElementProperties.RelativeTo != null)
            {
                return FindSubElement(subElementProperties.RelativeTo, timeout).FindSubElements(new Shared.Types.Alias() { Definition = subElementProperties.Definition }, timeout);
            }

            if (subElementProperties.ParentElement != null)
            {
                return FindSubElement(GetElementProperties(subElementProperties.ParentElement), timeout).FindSubElements(new Shared.Types.Alias() { Definition = subElementProperties.Definition }, timeout);
            }

            Stopwatch watch = new Stopwatch();
            watch.Start();
            IList<ITestableWebElement> elements = new List<ITestableWebElement>();

            //While the timeout has not elapsed attempt to find element
            while ((watch.ElapsedMilliseconds / 1000) < timeout)
            {
                elements = SearchContext.FindSubElements(subElementProperties.Definition);
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
            if (SubElements.ContainsKey(name))
            {
                throw new Exception($"Element with name {name} has already been registered on this object, please choose another name.");
            }

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
            SubElements.AddElementDefinition(name, properties.ToArray());
        }
        
        public void RegisterSubElement(string name, Shared.Types.Alias elementProperties)
        {
            if (SubElements.ContainsKey(name))
            {
                throw new Exception($"Element with name {name} has already been registered on this object, please choose another name.");
            }
            
            SubElements.Add(name, elementProperties);
        }
    }
}
