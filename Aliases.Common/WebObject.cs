using System;
using System.Collections.Generic;
using Aliases.Common.Shared;
using Aliases.Common.Shared.Types;

namespace Aliases.Common
{
    public abstract class WebObject : ISearchableWebObject
    {
        public ISearchableWebObject BaseObject { get; set; }

        public int DefaultActionTimeout { get; set; } = 60;

        protected WebObject(ISearchableWebObject baseObject)
        {
            BaseObject = baseObject;
        }

        public ITestableWebElement FindSubElement(Alias subElementProperties)
        {
            return BaseObject.FindSubElement(subElementProperties);
        }

        public ITestableWebElement FindSubElement(Alias subElementProperties, int timeout)
        {
            return BaseObject.FindSubElement(subElementProperties, timeout);
        }

        public IList<ITestableWebElement> FindSubElements(Alias subElementProperties)
        {
            return BaseObject.FindSubElements(subElementProperties);
        }

        public IList<ITestableWebElement> FindSubElements(Alias subElementProperties, int timeout)
        {
            return BaseObject.FindSubElements(subElementProperties, timeout);
        }

        public void Type(Alias alias, string text)
        {
            BaseObject.Type(alias, text);
        }

        public void Click(Alias alias)
        {
            BaseObject.Click(alias);
        }

        public void Select(Alias alias, string item, bool isValue = false)
        {
            BaseObject.Select(alias, item, isValue);
        }

        public bool WaitForAppear(Alias alias)
        {
            return BaseObject.WaitForAppear(alias, DefaultActionTimeout);
        }

        public bool WaitForAppear(Alias alias, int timeout)
        {
            return BaseObject.WaitForAppear(alias, timeout);
        }

        public bool WaitForDisappear(Alias alias)
        {
            return BaseObject.WaitForDisappear(alias, DefaultActionTimeout);
        }

        public bool WaitForDisappear(Alias alias, int timeout)
        {
            return BaseObject.WaitForDisappear(alias, timeout);
        }

        public bool IsDisplayed(Alias alias)
        {
            return BaseObject.IsDisplayed(alias);
        }

        public bool IsSelected(Alias alias)
        {
            return BaseObject.IsSelected(alias);
        }

        public void SetChecked(Alias alias, bool value)
        {
            BaseObject.SetChecked(alias, value);
        }

        public string Attribute(Alias alias, string attributeName)
        {
            return BaseObject.Attribute(alias, attributeName);
        }

        public string TagName(Alias alias)
        {
            return BaseObject.TagName(alias);
        }

        public void WaitForAttributeState(Alias alias, string attributeName, Func<string, bool> condition, int timeout)
        {
            BaseObject.WaitForAttributeState(alias, attributeName, condition, timeout);
        }

        public string InnerHtml(Alias alias)
        {
            return BaseObject.InnerHtml(alias);
        }

        public string Text(Alias alias)
        {
            return BaseObject.Text(alias);
        }

        public string CssValue(Alias alias, string propertyName)
        {
            return BaseObject.CssValue(alias, propertyName);
        }

        public ITestableWebElement Parent(Alias alias, int levels = 1)
        {
            return BaseObject.Parent(alias, levels);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alias">Element to look for</param>
        /// <param name="timeToLook">Seconds to wait for it to appear</param>
        public bool Exists(Alias alias, int timeToLook)
        {
            return BaseObject.Exists(alias, timeToLook);
        }
    }
}
