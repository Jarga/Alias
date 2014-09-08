using System.Collections.Generic;
using System.Diagnostics;
using OpenQA.Selenium;
using TestAutomation.Selenium.Helpers;
using TestAutomation.Shared;

namespace TestAutomation.Selenium
{
    public abstract class SeleniumWebObject : ISearchableWebObject
    {
        protected ISearchContext _searchContext;
        
        public ITestableWebElement FindSubElement(IDictionary<string, string> subElementProperties)
        {
            return _searchContext.FindSubElement(subElementProperties);
        }

        public ITestableWebElement FindSubElement(IDictionary<string, string> subElementProperties, int timeout)
        {
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
                catch (NoSuchElementException)
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

        public IList<ITestableWebElement> FindSubElements(IDictionary<string, string> subElementProperties)
        {
            return _searchContext.FindSubElements(subElementProperties);
        }
    }
}
