using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Automation.Common.Shared;
using Automation.Common.Shared.Exceptions;
using OpenQA.Selenium;

namespace Automation.Common.Drivers.Selenium.Helpers
{
    public static class FindExtensions
    {
        public static By BuildBy(string key, string value)
        {
            bool isContains = false;
            if (value.StartsWith("contains="))
            {
                value = value.Substring(9);
                isContains = true;
            }
            switch (key.ToLower())
            {
                case "class":
                case "classname":
                    return By.ClassName(value);
                case "cssselector":
                case "css":
                    return By.CssSelector(value);
                case "id":
                    return isContains 
                        ? By.CssSelector($"[id*={value}]")
                        : By.Id(value);
                case "linktext":
                    return isContains 
                                ? By.PartialLinkText(value) 
                                : By.LinkText(value);
                case "name":
                    return By.Name(value);
                case "partiallinktext":
                    return By.PartialLinkText(value);
                case "tagname":
                    //return By.TagName(value); //TODO: Figure out why this by clause sucks (e.g. Does not actually work)!
                    return By.XPath($".//{value}");
                case "xpath":
                    return By.XPath(value);
                case "text":
                case "innertext":
                    return isContains
                                ? By.XPath($".//*[contains(normalize-space(.), \"{value}\")]")
                                : By.XPath($".//*[normalize-space(.)=\"{value}\"]");
                case "type":
                    return By.XPath($".//*[@type='{value}']");
                case "href":
                    return isContains 
                        ? By.CssSelector($"[href*='{value}']") 
                        : By.CssSelector($"[href='{value}']");
                default:
                    throw new InvalidSearchPropertyException(
                        $"Invalid By Clause property {key.ToLower()} for Selenium Element!");
            }
        }

        public static By BuildCompositeXPathBy(IDictionary<string, string> keyValueDictionary)
        {
            var localLookup = new Dictionary<string, string>(keyValueDictionary ?? new Dictionary<string, string>());

            if (localLookup.Keys.Count == 0)
            {
                throw new ArgumentException("Invalid Element Properties passed to build By Clause!");
            }
            string tagKey = localLookup.Keys.FirstOrDefault(name => "Tag".Equals(name, StringComparison.OrdinalIgnoreCase) || "TagName".Equals(name, StringComparison.OrdinalIgnoreCase));

            string xpathExpression = $".//{(tagKey != null ? localLookup[tagKey] : "*")}";

            if (tagKey != null)
            {
                localLookup.Remove(tagKey);
            }

            foreach (string key in localLookup.Keys)
            {
                string value = localLookup[key];
                bool isContains = false;
                if (value.StartsWith("contains="))
                {
                    value = value.Substring(9);
                    isContains = true;
                }
                switch (key.ToLower())
                {
                    case "class":
                    case "classname":
                        xpathExpression += GetAttributeXPath("class", value, isContains);
                        continue;
                    case "id":
                        xpathExpression += GetAttributeXPath("id", value, isContains);
                        continue;
                    case "value":
                        xpathExpression += GetAttributeXPath("value", value, isContains);
                        continue;
                    case "name":
                        xpathExpression += GetAttributeXPath("name", value, isContains);
                        continue;
                    case "text":
                    case "innertext":
                        xpathExpression += (isContains
                                    ? $"[contains(normalize-space(.), \"{value}\")]"
                                    : $"[normalize-space(.)=\"{value}\"]");
                        continue;
                    case "type":
                        xpathExpression += GetAttributeXPath("type", value, isContains);
                        continue;
                    case "href":
                        xpathExpression += (isContains
                                    ? $"[contains(@href, \"{value}\")]"
                                    : $"[@href=\"{value}\"]");
                        continue;
                    default:
                        throw new InvalidSearchPropertyException($"Invalid Composite By Clause property {key.ToLower()} for Selenium Element!");
                }
            }
            return By.XPath(xpathExpression);
        }

        public static string GetAttributeXPath(string attribute, string value, bool isContains)
        {
            return isContains ? $"[contains(@{attribute}, '{value}')]" : $"[@{attribute}='{value}']";
        }

        public static ITestableWebElement FindSubElement(this ISearchContext baseObject, IDictionary<string, string> elementProperties)
        {
            var properties = elementProperties;

            if (properties.Keys.Count == 0)
            {
                throw new ElementNotRegisteredException("Element was not registered or has no properties assigned!");
            }
            else if (properties.Keys.Count == 1)
            {
                string key = properties.Keys.ElementAt(0);
                try
                {
                    return new SeleniumWebElement(baseObject.FindElement(BuildBy(key, properties[key])));
                }
                catch (NoSuchElementException e)
                {
                    throw new ObjectNotFoundException(e.Message, e.InnerException);
                }
            }
            else
            {
                try
                {
                    return new SeleniumWebElement(baseObject.FindElement(BuildCompositeXPathBy(elementProperties)));
                }
                catch (NoSuchElementException e)
                {
                    throw new ObjectNotFoundException(e.Message, e.InnerException);
                }
            }
        }

        public static IList<ITestableWebElement> FindSubElements(this ISearchContext baseObject, IDictionary<string, string> elementProperties)
        {
            var properties = elementProperties;

            if (properties.Keys.Count == 0)
            {
                throw new Exception("Invalid properties on sub element!");
            }
            else if (properties.Keys.Count == 1)
            {
                string key = properties.Keys.ElementAt(0);
                ReadOnlyCollection<IWebElement> elements = baseObject.FindElements(BuildBy(key, properties[key]));
                return elements.Count < 1 ? new List<ITestableWebElement>() : elements.Select(element => (ITestableWebElement)new SeleniumWebElement(element)).ToList();
            }
            else
            {
                IList<IWebElement> elements = baseObject.FindElements(BuildCompositeXPathBy(properties)); return elements.Count < 1 ? new List<ITestableWebElement>() : elements.Select(element => (ITestableWebElement)new SeleniumWebElement(element)).ToList();
            }
        }
    }

}
