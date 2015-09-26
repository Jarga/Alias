using System;
using Automation.Common.Initialization.Interfaces;
using Automation.Common.Output;
using Automation.Common.Shared;
using Automation.Common.Shared.Exceptions;

namespace Automation.Common
{
    /// <summary>
    /// This object acts as a proxy for the actual implementation object, it is intended to hide the specific framework implementation
    /// </summary>
    public class WebPage : WebElement, ITestableWebPage
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

        public T New<T>() where T : WebPage
        {
            var ctor = typeof(T).GetConstructor(new[] { typeof(ITestConfiguration) });

            if (ctor == null)
            {
                throw new Exception($"Unable to construct page for type {typeof (T)}, no constructor exists.");
            }

            //Rebuild configuration with new page that will have a clean SubElements property
            ITestConfiguration newConfig = TestConfiguration.Build(TestConfiguration.TestOutput, TestConfiguration.TestEnvironmentType, AsNew());
            return ctor.Invoke(new object[] { newConfig }) as T;
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


        public void EnsureElementLoaded(string verificationElement, string successText, string failedText, bool takeScreenshotOnSuccess = false)
        {
            try
            {
                FindSubElement(verificationElement, 120);
                if (takeScreenshotOnSuccess)
                {
                    TestConfiguration.TestOutput.WriteLineWithScreenshot(successText, GetScreenshot());
                }
                else
                {
                    TestConfiguration.TestOutput.WriteLine(successText);
                }
            }
            catch (ObjectNotFoundException)
            {
                TestConfiguration.TestOutput.WriteLineWithScreenshot(failedText, GetScreenshot());
                throw;
            }
        }

        public void Dispose()
        {
            BaseObject.Dispose();
        }
    }
}
