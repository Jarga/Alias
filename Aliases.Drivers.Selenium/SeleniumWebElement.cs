using System;
using System.Diagnostics;
using System.Threading;
using Aliases.Common.Shared;
using Aliases.Common.Shared.Exceptions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Aliases.Drivers.Selenium
{
    public class SeleniumWebElement : SeleniumWebObject, ITestableWebElement
    {
        public IWebElement BaseObject;

        public SeleniumWebElement(IWebElement baseObject) : base(baseObject)
        {
            BaseObject = baseObject;
        }

        public virtual void Clear()
        {
            EnsureElementFocus();
            BaseObject.Clear();
        }

        public virtual void Type(string text)
        {
            EnsureElementFocus();
            BaseObject.SendKeys(text);
        }

        public virtual void Click()
        {
            EnsureElementFocus();
            BaseObject.Click();
        }

        public virtual void Select(string item, bool isValue = false)
        {
            EnsureElementFocus();
            if (isValue)
            {
                new SelectElement(BaseObject).SelectByValue(item);
            }
            else
            {
                new SelectElement(BaseObject).SelectByText(item);
            }
        }

        public virtual bool WaitForAppear(int timeout)
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

        public virtual bool WaitForDisappear(int timeout)
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

        public virtual bool IsDisplayed()
        {
            EnsureElementFocus();
            return BaseObject.Displayed;
        }

        public virtual bool IsSelected()
        {
            EnsureElementFocus();
            return BaseObject.Selected;
        }

        public virtual bool IsEnabled()
        {
            EnsureElementFocus();
            return BaseObject.Enabled;
        }

        public virtual void SetChecked(bool value)
        {
            EnsureElementFocus();
            if (BaseObject.Selected != value)
            {
                BaseObject.Click();
            }

            if (BaseObject.Selected != value)
            {
                BaseObject.SendKeys(Keys.Space);
            }

            if (BaseObject.Selected != value)
            {
               throw new ActionFailedException($"Failed to set checked value to {value} for element!");
            }
        }

        public virtual void WaitForAttributeState(string attributeName, Func<string, bool> condition, int timeout)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while ((watch.ElapsedMilliseconds / 1000) < timeout)
            {
                if (condition(Attribute(attributeName)))
                {
                    return;
                }
                Thread.Sleep(200);
            }
            watch.Stop();
            throw new ActionTimeoutException($"Element failed to get to the expected state in {timeout} seconds.");
        }

        public virtual string Attribute(string attributeName)
        {
            EnsureElementFocus();
            return BaseObject.GetAttribute(attributeName);
        }

        public virtual string TagName()
        {
            EnsureElementFocus();
            return BaseObject.TagName;
        }

        public virtual string InnerHtml()
        {
            return Attribute("innerHTML");
        }

        public virtual string Text()
        {
            EnsureElementFocus();
            return BaseObject.Text;
        }

        public virtual string CssValue(string propertyName)
        {
            EnsureElementFocus();
            return BaseObject.GetCssValue(propertyName);
        }

        public virtual ITestableWebElement Parent(int levels = 1)
        {
            EnsureElementFocus();
            var xpath = "..";

            for (int i = 1; i < levels; i++)
            {
                xpath += "/..";
            }

            return new SeleniumWebElement(BaseObject.FindElement(By.XPath(xpath)));
        }
    }
}
