using System.Collections.Generic;
using Automation.Common.Shared.Types;

namespace Automation.Common.Shared
{
    public interface ISearchableWebObject
    {
        int DefaultActionTimeout { get; set; }
        IDictionary<string, Alias> SubElements { get; set; }

        Alias GetElementProperties(string targetElement);
        ITestableWebElement FindSubElement(string targetSubElement);
        ITestableWebElement FindSubElement(string targetSubElement, int timeout);
        ITestableWebElement FindSubElement(Alias subElementProperties);
        ITestableWebElement FindSubElement(Alias subElementProperties, int timeout);
        IList<ITestableWebElement> FindSubElements(string targetSubElement);
        IList<ITestableWebElement> FindSubElements(string targetSubElement, int timeout);
        IList<ITestableWebElement> FindSubElements(Alias subElementProperties);
        IList<ITestableWebElement> FindSubElements(Alias subElementProperties, int timeout);
        
        void RegisterSubElement(string name, dynamic elementProperties);
        void RegisterSubElement(string name, Alias elementProperties);
    }
}
