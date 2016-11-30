using System;
using System.Collections.Generic;
using System.Linq;
using Aliases.Common.Configuration;
using Aliases.Common.Output;
using Aliases.Common.Shared;
using Aliases.Common.Shared.Types;

namespace Aliases.Common
{
    /// <summary>
    /// This object acts as a proxy for the actual implementation object, it is intended to hide the specific framework implementation
    /// </summary>
    public class WebPage : WebObject, ITestableWebPage
    {
        public ITestConfiguration TestConfiguration { get; set; }

        public ITestOutput TestOutput => TestConfiguration.TestOutput;

        /// <summary>
        /// Hides WebElement base object to expose additional functions
        /// </summary>
        public new ITestableWebPage BaseObject => TestConfiguration.BaseTestPageType;

        public WebPage(ITestConfiguration testConfig) : base(testConfig.BaseTestPageType)
        {
            TestConfiguration = testConfig;
        }

        private void TryWithScreenshot(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                TestOutput.WriteLineWithScreenshot($"Action Failed due to exception. Exception {e.Message}\r\n{e.StackTrace}\r\n", GetScreenshot());
                throw;
            }
        }

        private T TryWithScreenshot<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch (Exception e)
            {
                TestOutput.WriteLineWithScreenshot($"Action Failed due to exception. Exception {e.Message}\r\n{e.StackTrace}\r\n", GetScreenshot());
                throw;
            }
        }

        public T New<T>(params object[] additionalParams) where T : WebPage
        {
            var ctorArgs = new List<object>();
            
            //Rebuild configuration with new page that will have a clean SubElements property
            ctorArgs.Add(TestConfiguration.Create(TestConfiguration.TestOutput, TestConfiguration.BaseTestUrl, TestConfiguration.TestEnvironmentType, AsNew()));

            if (additionalParams != null)
            {
                foreach (object param in additionalParams)
                {
                    ctorArgs.Add(param);
                }
                
            }

            var ctor = typeof(T).GetConstructor(ctorArgs.Select(o => o?.GetType()).ToArray());

            if (ctor == null)
            {
                throw new Exception($"Unable to construct page for type {typeof (T)}, no constructor exists.");
            }

            return ctor.Invoke(ctorArgs.ToArray()) as T;
        }

        public ITestableWebPage AsNew()
        {
            return BaseObject.AsNew();
        }

        public void EnsureFocus()
        {
            TryWithScreenshot(BaseObject.EnsureFocus);
        }

        public void SetActiveWindow(string windowUrlContains, int timeout)
        {
            TryWithScreenshot(() => BaseObject.SetActiveWindow(windowUrlContains, timeout));
        }

        public void SetActiveWindow(string windowUrlContains)
        {
            SetActiveWindow(windowUrlContains, DefaultActionTimeout);
        }

        public void Open(Uri uri)
        {
            TryWithScreenshot(() => BaseObject.Open(uri));
        }

        public void Open(string url)
        {
            TryWithScreenshot(() => BaseObject.Open(url));
        }

        public void Close()
        {
            TryWithScreenshot(BaseObject.Close);
        }

        public void Maximize()
        {
            TryWithScreenshot(BaseObject.Maximize);
        }

        public string GetCurrentUrl()
        {
            return TryWithScreenshot(() => BaseObject.GetCurrentUrl());
        }

        public string GetScreenshot()
        {
            return TryWithScreenshot(() => BaseObject.GetScreenshot());
        }

        public void EnsureElementLoaded(Alias verificationElement, string successText, string failedText, bool takeScreenshotOnSuccess = false)
        {
            try
            {
                FindSubElement(verificationElement, DefaultActionTimeout);
                if (!string.IsNullOrWhiteSpace(successText))
                {
                    if (takeScreenshotOnSuccess)
                    {
                        TestConfiguration.TestOutput.WriteLineWithScreenshot(successText, GetScreenshot());
                    }
                    else
                    {
                        TestConfiguration.TestOutput.WriteLine(successText);
                    }
                }
            }
            catch (Exception)
            {
                TestConfiguration.TestOutput.WriteLineWithScreenshot(
                    !string.IsNullOrWhiteSpace(failedText)
                        ? failedText
                        : $"failed to verify element {verificationElement}", GetScreenshot());
                throw;
            }
        }

        public void BrowserBack()
        {
            TryWithScreenshot(BaseObject.BrowserBack);
        }

        public IDialog Dialog()
        {
            return TryWithScreenshot(() => BaseObject.Dialog());
        }

        public ITestableWebPage Frame(Alias alias)
        {
            return TryWithScreenshot(() => BaseObject.Frame(alias));
        }

        public void Type(string text)
        {
            TryWithScreenshot(() => BaseObject.Type(text));
        }

        public void Click()
        {
            TryWithScreenshot(() => BaseObject.Click());
        }

        public bool WaitForAppear(int timeout)
        {
            return TryWithScreenshot(() => BaseObject.WaitForAppear(timeout));
        }

        public bool WaitForDisappear(int timeout)
        {
            return TryWithScreenshot(() => BaseObject.WaitForDisappear(timeout));
        }

        public bool IsDisplayed()
        {
            return TryWithScreenshot(BaseObject.IsDisplayed);
        }

        public void WaitForAttributeState(string attributeName, Func<string, bool> condition, int timeout)
        {
            TryWithScreenshot(() => BaseObject.WaitForAttributeState(attributeName, condition, timeout));
        }

        public string Attribute(string attributeName)
        {
            return TryWithScreenshot(() => BaseObject.Attribute(attributeName));
        }

        public string TagName()
        {
            return TryWithScreenshot(BaseObject.TagName);
        }

        public string InnerHtml()
        {
            return TryWithScreenshot(BaseObject.InnerHtml);
        }

        public void Hover(Alias alias)
        {
            TryWithScreenshot(() => BaseObject.Hover(alias));
        }

        public override ITestableWebElement FindSubElement(Alias subElementProperties)
        {
            return TryWithScreenshot(() => base.FindSubElement(subElementProperties));
        }

        public override ITestableWebElement FindSubElement(Alias subElementProperties, int timeout)
        {
            return TryWithScreenshot(() => base.FindSubElement(subElementProperties, timeout));
        }

        public override IList<ITestableWebElement> FindSubElements(Alias subElementProperties)
        {
            return TryWithScreenshot(() => base.FindSubElements(subElementProperties));
        }

        public override IList<ITestableWebElement> FindSubElements(Alias subElementProperties, int timeout)
        {
            return TryWithScreenshot(() => base.FindSubElements(subElementProperties, timeout));
        }

        public override void Type(Alias alias, string text)
        {
            TryWithScreenshot(() => base.Type(alias, text));
        }

        public override void Click(Alias alias)
        {
            TryWithScreenshot(() => base.Click(alias));
        }

        public override bool Exists(Alias alias, int timeToLook)
        {
            return TryWithScreenshot(() => base.Exists(alias));
        }

        public override void Select(Alias alias, string item, bool isValue = false)
        {
            TryWithScreenshot(() => base.Select(alias, item, isValue));
        }

        public override void SetChecked(Alias alias, bool value)
        {
            TryWithScreenshot(() => base.SetChecked(alias, value));
        }

        public override bool WaitForAppear(Alias alias, int timeout)
        {
            return TryWithScreenshot(() => base.WaitForAppear(alias, timeout));
        }

        public override bool WaitForDisappear(Alias alias, int timeout)
        {
            return TryWithScreenshot(() => base.WaitForDisappear(alias, timeout));
        }

        public override bool IsDisplayed(Alias alias)
        {
            return TryWithScreenshot(() => base.IsDisplayed(alias));
        }

        public override bool IsSelected(Alias alias)
        {
            return TryWithScreenshot(() => base.IsSelected(alias));
        }

        public override bool IsEnabled(Alias alias)
        {
            return TryWithScreenshot(() => base.IsEnabled(alias));
        }

        public override void WaitForAttributeState(Alias alias, string attributeName, Func<string, bool> condition, int timeout)
        {
            TryWithScreenshot(() => base.WaitForAttributeState(alias, attributeName, condition, timeout));
        }

        public override string Attribute(Alias alias, string attributeName)
        {
            return TryWithScreenshot(() => base.Attribute(alias, attributeName));
        }

        public override string TagName(Alias alias)
        {
            return TryWithScreenshot(() => base.TagName(alias));
        }

        public override string InnerHtml(Alias alias)
        {
            return TryWithScreenshot(() => base.InnerHtml(alias));
        }

        public override string Text(Alias alias)
        {
            return TryWithScreenshot(() => base.Text(alias));
        }

        public override string CssValue(Alias alias, string propertyName)
        {
            return TryWithScreenshot(() => base.CssValue(alias, propertyName));
        }

        public override ITestableWebElement Parent(Alias alias, int levels = 1)
        {
            return TryWithScreenshot(() => base.Parent(alias, levels));
        }

        public void Dispose()
        {
            BaseObject.Dispose();
        }
    }
}
