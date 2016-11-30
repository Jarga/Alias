using Aliases.Common.Shared;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliases.Drivers.Selenium
{
    public class SeleniumWebFrame : SeleniumWebPage
    {
        public ITestableWebPage ParentFrame;
        public IWebElement FrameRoot;

        public SeleniumWebFrame(SeleniumWebPage parent, IWebElement frameRoot) : base(parent.Driver)
        {
            FrameRoot = frameRoot;
            ParentFrame = parent;
        }

        public override void EnsureElementFocus()
        {
            Driver.SwitchTo().Frame(FrameRoot);
        }
    }
}
