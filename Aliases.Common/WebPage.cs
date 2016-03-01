﻿using System;
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
            BaseObject.EnsureFocus();
        }

        public void SetActiveWindow(string windowUrlContains, int timeout)
        {
            BaseObject.SetActiveWindow(windowUrlContains, timeout);
        }

        public void SetActiveWindow(string windowUrlContains)
        {
            BaseObject.SetActiveWindow(windowUrlContains, DefaultActionTimeout);
        }

        public void Open(Uri uri)
        {
            BaseObject.Open(uri);
        }

        public void Open(string url)
        {
            BaseObject.Open(url);
        }

        public void Close()
        {
            BaseObject.Close();
        }

        public void Maximize()
        {
            BaseObject.Maximize();
        }

        public string GetCurrentUrl()
        {
            return BaseObject.GetCurrentUrl();
        }

        public string GetScreenshot()
        {
            return BaseObject.GetScreenshot();
        }

        public void EnsureElementLoaded(Alias verificationElement, string successText, string failedText, bool takeScreenshotOnSuccess = false)
        {
            try
            {
                FindSubElement(verificationElement, 120);
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

        public void Dispose()
        {
            BaseObject.Dispose();
        }

        public void BrowserBack()
        {
            BaseObject.BrowserBack();
        }

        public IDialog Dialog()
        {
            return BaseObject.Dialog();
        }

        public void Type(string text)
        {
            BaseObject.Type(text);
        }

        public void Click()
        {
            BaseObject.Click();
        }

        public bool WaitForAppear(int timeout)
        {
            return BaseObject.WaitForAppear(timeout);
        }

        public bool WaitForDisappear(int timeout)
        {
            return BaseObject.WaitForDisappear(timeout);
        }

        public bool IsDisplayed()
        {
            return BaseObject.IsDisplayed();
        }

        public void WaitForAttributeState(string attributeName, Func<string, bool> condition, int timeout)
        {
            BaseObject.WaitForAttributeState(attributeName, condition, timeout);
        }

        public string Attribute(string attributeName)
        {
            return BaseObject.Attribute(attributeName);
        }

        public string TagName()
        {
            return BaseObject.TagName();
        }

        public string InnerHtml()
        {
            return BaseObject.InnerHtml();
        }
    }
}