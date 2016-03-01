using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Aliases.Common.Shared;
using Aliases.Common.Shared.Exceptions;
using Aliases.Common.Shared.Types;
using Aliases.Drivers.Selenium.Helpers;
using OpenQA.Selenium;

namespace Aliases.Drivers.Selenium
{
    public class SeleniumWebObject : ISearchableWebObject
    {
        protected ISearchContext SearchContext;

        public int DefaultActionTimeout { get; set; } = 60;

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

        public ITestableWebElement FindSubElement(Alias subElementProperties)
        {
            EnsureElementFocus();

            //If this element uses another element as a base then find the other element and search from there
            if (subElementProperties.RelativeTo != null)
            {
                return FindSubElement(subElementProperties.RelativeTo).FindSubElement(new Alias() { Definition = subElementProperties.Definition });
            }

            return SearchContext.FindSubElement(subElementProperties.Definition);
        }

        public ITestableWebElement FindSubElement(Alias subElementProperties, int timeout)
        {
            //If this element uses another element as a base then find the other element and search from there
            if (subElementProperties.RelativeTo != null)
            {
                return FindSubElement(subElementProperties.RelativeTo, timeout).FindSubElement(new Alias() { Definition = subElementProperties.Definition }, timeout);
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
                    throw;
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

        public IList<ITestableWebElement> FindSubElements(Alias subElementProperties)
        {
            EnsureElementFocus();

            //If this element uses another element as a base then find the other element and search from there
            if (subElementProperties.RelativeTo != null)
            {
                return FindSubElement(subElementProperties.RelativeTo).FindSubElements(new Alias() { Definition = subElementProperties.Definition });
            }

            return SearchContext.FindSubElements(subElementProperties.Definition);
        }

        public IList<ITestableWebElement> FindSubElements(Alias subElementProperties, int timeout)
        {
            //If this element uses another element as a base then find the other element and search from there
            if (subElementProperties.RelativeTo != null)
            {
                return FindSubElement(subElementProperties.RelativeTo, timeout).FindSubElements(new Alias() { Definition = subElementProperties.Definition }, timeout);
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

        public void Type(Alias alias, string text)
        {
            FindSubElement(alias).Type(text);
        }

        public void Click(Alias alias)
        {
            FindSubElement(alias).Click();
        }

        public bool Exists(Alias alias, int timeout)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while ((watch.ElapsedMilliseconds / 1000) < timeout)
            {
                var elementsFound = FindSubElements(alias);
                if (elementsFound.Any())
                {
                    watch.Stop();
                    return true;
                }
            }
            watch.Stop();
            return false;
        }

        public void Select(Alias alias, string item, bool isValue = false)
        {
            FindSubElement(alias).Select(item, isValue);
        }

        public bool WaitForAppear(Alias alias, int timeout)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while ((watch.ElapsedMilliseconds / 1000) < timeout)
            {
                IList<ITestableWebElement> elementsFound = FindSubElements(alias);
                if (elementsFound.Any())
                {
                    bool allElementsAreVisible = true;

                    foreach (ITestableWebElement element in elementsFound)
                    {
                        allElementsAreVisible = allElementsAreVisible && element.IsDisplayed();
                    }

                    if (allElementsAreVisible)
                    {
                        watch.Stop();
                        return true;
                    }
                }
                Thread.Sleep(200);
            }
            watch.Stop();
            return false;
        }

        public bool WaitForDisappear(Alias alias, int timeout)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while ((watch.ElapsedMilliseconds / 1000) < timeout)
            {
                IList<ITestableWebElement> elementsFound = FindSubElements(alias);
                if (elementsFound.Any())
                {
                    bool allElementsAreHidden = true;

                    foreach (ITestableWebElement element in elementsFound)
                    {
                        allElementsAreHidden = allElementsAreHidden && !element.IsDisplayed();
                    }

                    if (allElementsAreHidden)
                    {
                        watch.Stop();
                        return true;
                    }
                }
                Thread.Sleep(200);
            }
            watch.Stop();
            return false;
        }

        public bool IsDisplayed(Alias alias)
        {
            return FindSubElement(alias).IsDisplayed();
        }

        public bool IsSelected(Alias alias)
        {
            return FindSubElement(alias).IsSelected();
        }

        public void SetChecked(Alias alias, bool value)
        {
            FindSubElement(alias).SetChecked(value);
        }

        public void WaitForAttributeState(Alias alias, string attributeName, Func<string, bool> condition, int timeout)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while ((watch.ElapsedMilliseconds / 1000) < timeout)
            {
                IList<ITestableWebElement> elementsFound = FindSubElements(alias);
                if (elementsFound.Any())
                {
                    bool allElementHaveState = true;

                    foreach (ITestableWebElement element in elementsFound)
                    {
                        allElementHaveState = allElementHaveState && condition(element.Attribute(attributeName));
                    }

                    if (allElementHaveState)
                    {
                        watch.Stop();
                        return;
                    }
                }
                else
                {
                    throw new ObjectNotFoundException($"Unable to find {alias.AliasName ?? string.Join(";", alias.Definition)}, please check your object definitions.");
                }
                Thread.Sleep(200);
            }
            watch.Stop();
            throw new ActionTimeoutException($"Element {alias.AliasName ?? string.Join(";", alias.Definition)} failed to get to the expected state in {timeout} seconds.");
        }

        public string Attribute(Alias alias, string attributeName)
        {
            return FindSubElement(alias).Attribute(attributeName);
        }

        public string TagName(Alias alias)
        {
            return FindSubElement(alias).TagName();
        }

        public string InnerHtml(Alias alias)
        {
            return FindSubElement(alias).InnerHtml();
        }

        public string Text(Alias alias)
        {
            return FindSubElement(alias).Text();
        }

        public string CssValue(Alias alias, string propertyName)
        {
            return FindSubElement(alias).CssValue(propertyName);
        }

        public ITestableWebElement Parent(Alias alias, int levels = 1)
        {
            return FindSubElement(alias).Parent(levels);
        }
    }
}
