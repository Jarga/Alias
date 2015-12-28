using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Automation.Common.Shared;
using Automation.Common.Shared.Types;

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

        public void Type(Alias alias, string text)
        {
            Type(FindSubElement(alias), text);
        }

        public void Click()
        {
            BaseObject.Click();
        }

        public void Click(ITestableWebElement element)
        {
            BaseObject.Click(element);
        }

        public void Click(string targetSubElement)
        {
            Click(FindSubElement(targetSubElement));
        }

        public void Click(Alias alias)
        {
            Click(FindSubElement(alias));
        }

        public void Select(string item, bool isValue = false)
        {
            BaseObject.Select(item, isValue);
        }

        public void Select(ITestableWebElement element, string item, bool isValue = false)
        {
            BaseObject.Select(element, item, isValue);
        }

        public void Select(string targetSubElement, string item, bool isValue = false)
        {
            Select(FindSubElement(targetSubElement), item, isValue);
        }

        public void Select(Alias alias, string item, bool isValue = false)
        {
            Select(FindSubElement(alias), item, isValue);
        }

        public bool WaitForAppear()
        {
            return BaseObject.WaitForAppear(DefaultActionTimeout);
        }

        public bool WaitForAppear(int timeout)
        {
            return BaseObject.WaitForAppear(timeout);
        }

        public bool WaitForAppear(string targetSubElement, int timeout)
        {
            return BaseObject.WaitForAppear(targetSubElement, timeout);
        }

        public bool WaitForAppear(Alias alias, int timeout)
        {
            return BaseObject.WaitForAppear(alias, timeout);
        }

        public bool WaitForDisappear()
        {
            return BaseObject.WaitForDisappear(DefaultActionTimeout);
        }

        public bool WaitForDisappear(int timeout)
        {
            return BaseObject.WaitForDisappear(timeout);
        }

        public bool WaitForDisappear(string targetSubElement, int timeout)
        {
            return BaseObject.WaitForDisappear(targetSubElement, timeout);
        }

        public bool WaitForDisappear(Alias alias, int timeout)
        {
            return BaseObject.WaitForDisappear(alias, timeout);
        }

        public bool IsDisplayed()
        {
            return BaseObject.IsDisplayed();
        }

        public bool IsSelected()
        {
            return BaseObject.IsSelected();
        }
        public bool IsSelected(string targetSubElement)
        {
            return FindSubElement(targetSubElement).IsSelected();
        }

        public bool IsSelected(Alias alias)
        {
            return FindSubElement(alias).IsSelected();
        }

        public void SetChecked(bool value)
        {
            BaseObject.SetChecked(value);
        }

        public void SetChecked(ITestableWebElement element, bool value)
        {
            BaseObject.SetChecked(element, value);
        }

        public void SetChecked(string targetSubElement, bool value)
        {
            BaseObject.SetChecked(FindSubElement(targetSubElement), value);
        }

        public void SetChecked(Alias alias, bool value)
        {
            BaseObject.SetChecked(FindSubElement(alias), value);
        }

        public void WaitForAttributeState(string attributeName, Func<string, bool> condition, int timeout)
        {
            BaseObject.WaitForAttributeState(attributeName, condition, timeout);
        }

        public void WaitForAttributeState(string targetSubElement, string attributeName, Func<string, bool> condition, int timeout)
        {
            BaseObject.WaitForAttributeState(targetSubElement, attributeName, condition, timeout);
        }

        public void WaitForAttributeState(Alias alias, string attributeName, Func<string, bool> condition, int timeout)
        {
            BaseObject.WaitForAttributeState(alias, attributeName, condition, timeout);
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

        public string GetText()
        {
            return BaseObject.GetText();
        }

        public string GetCssValue(string propertyName)
        {
            return BaseObject.GetCssValue(propertyName);
        }

        public ITestableWebElement Parent(int levels = 1)
        {
            return BaseObject.Parent(levels);
        }

        public bool Exists(string targetSubElement)
        {
            return Exists(targetSubElement, this.DefaultActionTimeout);
        }

        public bool WaitForExists(string targetSubElement, int timeToLook = 60)
        {
            return Exists(targetSubElement, timeToLook);
        }

        public bool WaitForExists(Alias alias, int timeToLook = 60)
        {
            return Exists(alias, timeToLook);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetSubElement">Element to look for</param>
        /// <param name="timeToLook">Seconds to wait for it to appear</param>
        /// <returns></returns>
        public bool Exists(string targetSubElement, int timeToLook)
        {
            return Exists(GetElementProperties(targetSubElement), timeToLook);
        }

        public bool Exists(Alias alias, int timeToLook)
        {
            return BaseObject.Exists(alias, timeToLook);
        }

        public int DefaultActionTimeout
        {
            get { return BaseObject.DefaultActionTimeout; }
            set { BaseObject.DefaultActionTimeout = value; }
        }

        public IDictionary<string, Alias> SubElements
        {
            get { return BaseObject.SubElements; }
            set { BaseObject.SubElements = value; }
        }

        public Alias GetElementProperties(string targetElement)
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

        public ITestableWebElement FindSubElement(Alias subElementProperties)
        {
            return BaseObject.FindSubElement(subElementProperties);
        }

        public ITestableWebElement FindSubElement(Alias subElementProperties, int timeout)
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

        public IList<ITestableWebElement> FindSubElements(Alias subElementProperties)
        {
            return BaseObject.FindSubElements(subElementProperties);
        }

        public IList<ITestableWebElement> FindSubElements(Alias subElementProperties, int timeout)
        {
            return BaseObject.FindSubElements(subElementProperties, timeout);
        }

        public void RegisterSubElement(string name, dynamic elementProperties)
        {
            BaseObject.RegisterSubElement(name, elementProperties as object);
        }

        public void RegisterSubElement(string name, Alias elementProperties)
        {
            BaseObject.RegisterSubElement(name, elementProperties);
        }
    }
}
