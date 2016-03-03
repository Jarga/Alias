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

        public virtual ITestableWebElement FindSubElement(Alias subElementProperties)
        {
            return FindSubElement(subElementProperties, DefaultActionTimeout);
        }

        public virtual ITestableWebElement FindSubElement(Alias subElementProperties, int timeout)
        {
            return BaseObject.FindSubElement(subElementProperties, timeout);
        }

        public virtual IList<ITestableWebElement> FindSubElements(Alias subElementProperties)
        {
            return FindSubElements(subElementProperties, DefaultActionTimeout);
        }

        public virtual IList<ITestableWebElement> FindSubElements(Alias subElementProperties, int timeout)
        {
            return BaseObject.FindSubElements(subElementProperties, timeout);
        }

        public virtual void Type(Alias alias, string text)
        {
            BaseObject.Type(alias, text);
        }

        public virtual void Click(Alias alias)
        {
            BaseObject.Click(alias);
        }

        public virtual void Select(Alias alias, string item, bool isValue = false)
        {
            BaseObject.Select(alias, item, isValue);
        }

        public virtual bool WaitForAppear(Alias alias)
        {
            return BaseObject.WaitForAppear(alias, DefaultActionTimeout);
        }

        public virtual bool WaitForAppear(Alias alias, int timeout)
        {
            return BaseObject.WaitForAppear(alias, timeout);
        }

        public virtual bool WaitForDisappear(Alias alias)
        {
            return BaseObject.WaitForDisappear(alias, DefaultActionTimeout);
        }

        public virtual bool WaitForDisappear(Alias alias, int timeout)
        {
            return BaseObject.WaitForDisappear(alias, timeout);
        }

        public virtual bool IsDisplayed(Alias alias)
        {
            return BaseObject.IsDisplayed(alias);
        }

        public virtual bool IsSelected(Alias alias)
        {
            return BaseObject.IsSelected(alias);
        }

        public virtual void SetChecked(Alias alias, bool value)
        {
            BaseObject.SetChecked(alias, value);
        }

        public virtual string Attribute(Alias alias, string attributeName)
        {
            return BaseObject.Attribute(alias, attributeName);
        }

        public virtual string TagName(Alias alias)
        {
            return BaseObject.TagName(alias);
        }

        public virtual void WaitForAttributeState(Alias alias, string attributeName, Func<string, bool> condition, int timeout)
        {
            BaseObject.WaitForAttributeState(alias, attributeName, condition, timeout);
        }

        public virtual string InnerHtml(Alias alias)
        {
            return BaseObject.InnerHtml(alias);
        }

        public virtual string Text(Alias alias)
        {
            return BaseObject.Text(alias);
        }

        public virtual string CssValue(Alias alias, string propertyName)
        {
            return BaseObject.CssValue(alias, propertyName);
        }

        public virtual ITestableWebElement Parent(Alias alias, int levels = 1)
        {
            return BaseObject.Parent(alias, levels);
        }

        public virtual bool Exists(Alias alias)
        {
            return BaseObject.Exists(alias, DefaultActionTimeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alias">Element to look for</param>
        /// <param name="timeToLook">Seconds to wait for it to appear</param>
        public virtual bool Exists(Alias alias, int timeToLook)
        {
            return BaseObject.Exists(alias, timeToLook);
        }
    }
}
