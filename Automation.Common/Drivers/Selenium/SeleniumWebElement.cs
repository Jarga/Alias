using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Alias.Common.Shared;
using Alias.Common.Shared.Exceptions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Alias.Common.Drivers.Selenium
{
    public class SeleniumWebElement : SeleniumWebObject, ITestableWebElement
    {
        public IWebElement _baseObject;

        public SeleniumWebElement(IWebElement baseObject)
        {
            this._baseObject = baseObject;
            this.SearchContext = baseObject;
        }

        public void Clear()
        {
            EnsureElementFocus();
            _baseObject.Clear();
        }

        public void Type(string text)
        {
            EnsureElementFocus();
            _baseObject.SendKeys(text);
        }

        public void Type(ITestableWebElement element, string text)
        {
            element.Type(text);
        }

        public void Click()
        {
            EnsureElementFocus();
            _baseObject.Click();
        }

        public void Click(ITestableWebElement element)
        {
            element.Click();
        }

        public bool Exists(string targetSubElement, int timeout)
        {
            return Exists(GetElementProperties(targetSubElement), timeout);
        }

        public bool Exists(Shared.Types.Alias alias, int timeout)
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

        public void Select(string item, bool isValue = false)
        {
            EnsureElementFocus();
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

        public bool WaitForAppear(int timeout)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while ((watch.ElapsedMilliseconds / 1000) < timeout)
            {
                if (IsDisplayed())
                {
                    return true;
                }
                Thread.Sleep(200);
            }
            watch.Stop();
            return false;
        }

        public bool WaitForAppear(string targetSubElement, int timeout)
        {
            return WaitForAppear(GetElementProperties(targetSubElement), timeout);
        }


        public bool WaitForAppear(Shared.Types.Alias alias, int timeout)
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

        public bool WaitForDisappear(int timeout)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while ((watch.ElapsedMilliseconds / 1000) < timeout)
            {
                if (!IsDisplayed())
                {
                    return true;
                }
                Thread.Sleep(200);
            }
            watch.Stop();
            return false;
        }
        public bool WaitForDisappear(string targetSubElement, int timeout)
        {
            return WaitForDisappear(GetElementProperties(targetSubElement), timeout);
        }

        public bool WaitForDisappear(Shared.Types.Alias alias, int timeout)
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

        public bool IsSelected()
        {
            EnsureElementFocus();
            return _baseObject.Selected;
        }

        public void SetChecked(bool value)
        {
            EnsureElementFocus();
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
               throw new ActionFailedException($"Failed to set checked value to {value} for element!");
            }
        }

        public void SetChecked(ITestableWebElement element, bool value)
        {
            element.SetChecked(value);
        }

        public void WaitForAttributeState(string attributeName, Func<string, bool> condition, int timeout)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while ((watch.ElapsedMilliseconds / 1000) < timeout)
            {
                if (condition(GetAttribute(attributeName)))
                {
                    return;
                }
                Thread.Sleep(200);
            }
            watch.Stop();
            throw new ActionTimeoutException($"Element failed to get to the expected state in {timeout} seconds.");
        }

        public void WaitForAttributeState(string targetSubElement, string attributeName, Func<string, bool> condition, int timeout)
        {
            WaitForAttributeState(GetElementProperties(targetSubElement), attributeName, condition, timeout);
        }

        public void WaitForAttributeState(Shared.Types.Alias alias, string attributeName, Func<string, bool> condition, int timeout)
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
                    throw new ObjectNotFoundException($"Unable to find {alias.AliasName ?? string.Join(";", alias.Definition)}, please check your object definitions.");
                }
                Thread.Sleep(200);
            }
            watch.Stop();
            throw new ActionTimeoutException($"Element {alias.AliasName ?? string.Join(";", alias.Definition)} failed to get to the expected state in {timeout} seconds.");
        }

        public bool IsDisplayed()
        {
            EnsureElementFocus();
            return _baseObject.Displayed;
        }

        public string GetAttribute(string attributeName)
        {
            EnsureElementFocus();
            return _baseObject.GetAttribute(attributeName);
        }

        public string GetTagName()
        {
            EnsureElementFocus();
            return _baseObject.TagName;
        }

        public string InnerHtml()
        {
            return GetAttribute("innerHTML");
        }

        public string GetText()
        {
            return _baseObject.Text;
        }

        public string GetCssValue(string propertyName)
        {
            return _baseObject.GetCssValue(propertyName);
        }

        public ITestableWebElement Parent(int levels = 1)
        {
            EnsureElementFocus();
            var xpath = "..";

            for (int i = 1; i < levels; i++)
            {
                xpath += "/..";
            }

            return new SeleniumWebElement(_baseObject.FindElement(By.XPath(xpath)));
        }
    }
}
