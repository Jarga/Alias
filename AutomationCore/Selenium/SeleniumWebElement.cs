using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutomationCore.Shared;
using AutomationCore.Shared.Exceptions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationCore.Selenium
{
    public class SeleniumWebElement : SeleniumWebObject, ITestableWebElement
    {
        public IWebElement _baseObject;

        public SeleniumWebElement(IWebElement baseObject)
        {
            this._baseObject = baseObject;
            this._searchContext = baseObject;
        }

        public void Clear()
        {
            _baseObject.Clear();
        }

        public void Type(string text)
        {
            _baseObject.SendKeys(text);
        }

        public void Type(ITestableWebElement element, string text)
        {
            element.Type(text);
        }

        public void Click()
        {
            _baseObject.Click();
        }

        public void Click(ITestableWebElement element)
        {
            element.Click();
        }

        public void Select(string item, bool isValue = false)
        {
            if (isValue)
            {
                new SelectElement(_baseObject).SelectByValue(item);
            }
            else
            {
                new SelectElement(_baseObject).SelectByText(item);
            }
        }

        public void Select(ITestableWebElement element, string item, bool isValue = false)
        {
            element.Select(item, isValue);
        }

        public bool WaitForAppear(string targetSubElement, int timeout)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while ((watch.ElapsedMilliseconds / 1000) < timeout)
            {
                IList<ITestableWebElement> elementsFound = FindSubElements(targetSubElement);
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
            }
            watch.Stop();
            return false;
        }

        public bool WaitForDisappear(string targetSubElement, int timeout)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while ((watch.ElapsedMilliseconds / 1000) < timeout)
            {
                IList<ITestableWebElement> elementsFound = FindSubElements(targetSubElement);
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
            }
            watch.Stop();
            return false;
        }

        public bool IsSelected()
        {
            return _baseObject.Selected;
        }

        public void SetChecked(bool value)
        {
            if (_baseObject.Selected != value)
            {
                _baseObject.Click();
            }

            if (_baseObject.Selected != value)
            {
                _baseObject.SendKeys(Keys.Space);
            }

            if (_baseObject.Selected != value)
            {
               throw new ActionFailedException(string.Format("Failed to set checked value to {0} for element!", value));
            }
        }

        public void SetChecked(ITestableWebElement element, bool value)
        {
            element.SetChecked(value);
        }

        public void WaitForAttributeState(string targetSubElement, string attributeName, Func<string, bool> condition, int timeout)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while ((watch.ElapsedMilliseconds / 1000) < timeout)
            {
                IList<ITestableWebElement> elementsFound = FindSubElements(targetSubElement);
                if (elementsFound.Any())
                {
                    bool allElementHaveState = true;

                    foreach (ITestableWebElement element in elementsFound)
                    {
                        allElementHaveState = allElementHaveState && condition(element.GetAttribute(attributeName));
                    }

                    if (allElementHaveState)
                    {
                        watch.Stop();
                        return;
                    }
                }
                else
                {
                    throw new ObjectNotFoundException(string.Format("Unable to find {0}, please check your object definitions.", targetSubElement));
                }
            }
            watch.Stop();
            throw new ActionTimeoutException(string.Format("Element {0} failed to get to the expected state in {1} seconds.", targetSubElement, timeout));
        }

        public bool IsDisplayed()
        {
            return _baseObject.Displayed;
        }

        public string GetAttribute(string attributeName)
        {
            return _baseObject.GetAttribute(attributeName);
        }

        public string GetTagName()
        {
            return _baseObject.TagName;
        }

        public string InnerHtml()
        {
            return GetAttribute("innerHTML");
        }

        public ITestableWebElement Parent(int levels = 1)
        {
            var xpath = "..";

            for (int i = 1; i < levels; i++)
            {
                xpath += "/..";
            }

            return new SeleniumWebElement(_baseObject.FindElement(By.XPath(xpath)));
        }
    }
}
