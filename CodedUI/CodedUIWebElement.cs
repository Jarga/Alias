using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UITesting;
using TestAutomation.Shared;
namespace TestAutomation.CodedUI
{
    class CodedUIWebElement : CodedUIWebObject, ITestableWebElement
    {

        public CodedUIWebElement(UITestControl baseObject)
        {
            _baseObject = baseObject;
        }

        public void Type(string text)
        {
            Keyboard.SendKeys(_baseObject, text);
        }

        public void Type(ITestableWebElement element, string text)
        {
            element.Type(text);
        }

        public void Click()
        {
            Mouse.Click(_baseObject);
        }

        public void Click(ITestableWebElement element)
        {
            element.Click();
        }
    }
}
