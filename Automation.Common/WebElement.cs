using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Automation.Common.Shared;

namespace Automation.Common
{
    public class WebElement : ITestableWebElement
    {
        public ITestableWebElement BaseObject { get; set; }

        public WebElement(ITestableWebElement baseObject)
        {
            BaseObject = baseObject;
        }

        public void Clear()
        {
            BaseObject.Clear();
        }

        public void Type(string text)
        {
            BaseObject.Type(text);
        }

        public void Type(ITestableWebElement element, string text)
        {
            BaseObject.Type(element, text);
        }

        public void Type(string targetSubElement, string text)
        {
            Type(FindSubElement(targetSubElement), text);
        }

        public void Click()
        {
            BaseObject.Click();
        }

        public void Click(ITestableWebElement element)
        {
            BaseObject.Click(element);
        }

        public void Select(string item, bool isValue = false)
        {
            BaseObject.Select(item, isValue);
        }

        public void Select(ITestableWebElement element, string item, bool isValue = false)
        {
            BaseObject.Select(element, item, isValue);
        }

        public bool WaitForAppear(string targetSubElement, int timeout)
        {
            return BaseObject.WaitForAppear(targetSubElement, timeout);
        }

        public bool WaitForDisappear(string targetSubElement, int timeout)
        {
            return BaseObject.WaitForDisappear(targetSubElement, timeout);
        }

        public bool IsDisplayed()
        {
            return BaseObject.IsDisplayed();
        }

        public bool IsSelected()
        {
            return BaseObject.IsSelected();
        }

        public void SetChecked(bool value)
        {
            BaseObject.SetChecked(value);
        }

        public void SetChecked(ITestableWebElement element, bool value)
        {
            BaseObject.SetChecked(element, value);
        }

        public void WaitForAttributeState(string targetSubElement, string attributeName, Func<string, bool> condition, int timeout)
        {
            BaseObject.WaitForAttributeState(targetSubElement, attributeName, condition, timeout);
        }

        public string GetAttribute(string attributeName)
        {
            return BaseObject.GetAttribute(attributeName);
        }

        public string GetTagName()
        {
            return BaseObject.GetTagName();
        }

        public string InnerHtml()
        {
            return BaseObject.InnerHtml();
        }

        public ITestableWebElement Parent(int levels = 1)
        {
            return BaseObject.Parent(levels);
        }

        public void Click(string targetSubElement)
        {
            Click(FindSubElement(targetSubElement));
        }

        public bool Exists(string targetSubElement)
        {
            return Exists(targetSubElement, this.DefaultActionTimeout);
        }

        public bool WaitForExists(string targetSubElement, int timeToLook = 60)
        {
            return Exists(targetSubElement, timeToLook);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetSubElement">Element to look for</param>
        /// <param name="timeToLook">Seconds to wait for it to appear</param>
        /// <returns></returns>
        public bool Exists(string targetSubElement, int timeToLook)
        {

            Stopwatch watch = new Stopwatch();
            watch.Start();

            while ((watch.ElapsedMilliseconds / 1000) < timeToLook)
            {
                var elementsFound = FindSubElements(targetSubElement);
                if (elementsFound.Any())
                {
                    watch.Stop();
                    return true;
                }
            }
            watch.Stop();
            return false;
        }

        public int DefaultActionTimeout
        {
            get { return BaseObject.DefaultActionTimeout; }
            set { BaseObject.DefaultActionTimeout = value; }
        }

        public IDictionary<string, IDictionary<string, string>> SubElements
        {
            get { return BaseObject.SubElements; }
            set { BaseObject.SubElements = value; }
        }

        public IDictionary<string, string> GetElementProperties(string targetElement)
        {
            return BaseObject.GetElementProperties(targetElement);
        }

        public ITestableWebElement FindSubElement(string targetSubElement)
        {
            return BaseObject.FindSubElement(targetSubElement);
        }

        public ITestableWebElement FindSubElement(string targetSubElement, int timeout)
        {
            return BaseObject.FindSubElement(targetSubElement, timeout);
        }

        public ITestableWebElement FindSubElement(IDictionary<string, string> subElementProperties)
        {
            return BaseObject.FindSubElement(subElementProperties);
        }

        public ITestableWebElement FindSubElement(IDictionary<string, string> subElementProperties, int timeout)
        {
            return BaseObject.FindSubElement(subElementProperties, timeout);
        }

        public IList<ITestableWebElement> FindSubElements(string targetSubElement)
        {
            return BaseObject.FindSubElements(targetSubElement);
        }

        public IList<ITestableWebElement> FindSubElements(string targetSubElement, int timeout)
        {
            return BaseObject.FindSubElements(targetSubElement, timeout);
        }

        public IList<ITestableWebElement> FindSubElements(IDictionary<string, string> subElementProperties)
        {
            return BaseObject.FindSubElements(subElementProperties);
        }

        public IList<ITestableWebElement> FindSubElements(IDictionary<string, string> subElementProperties, int timeout)
        {
            return BaseObject.FindSubElements(subElementProperties, timeout);
        }

        public void RegisterSubElement(string name, dynamic elementProperties)
        {
            BaseObject.RegisterSubElement(name, elementProperties as object);
        }

        public void RegisterSubElement(string name, params string[] elementProperties)
        {
            BaseObject.RegisterSubElement(name, elementProperties);
        }

        public void RegisterSubElement(string name, IDictionary<string, string> elementProperties)
        {
            BaseObject.RegisterSubElement(name, elementProperties);
        }
    }
}
