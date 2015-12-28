using System.Collections.Generic;

namespace Alias.Common.Shared
{
    public interface ISearchableWebObject
    {
        int DefaultActionTimeout { get; set; }
        IDictionary<string, Types.Alias> SubElements { get; set; }

        Types.Alias GetElementProperties(string targetElement);
        ITestableWebElement FindSubElement(string targetSubElement);
        ITestableWebElement FindSubElement(string targetSubElement, int timeout);
        ITestableWebElement FindSubElement(Types.Alias subElementProperties);
        ITestableWebElement FindSubElement(Types.Alias subElementProperties, int timeout);
        IList<ITestableWebElement> FindSubElements(string targetSubElement);
        IList<ITestableWebElement> FindSubElements(string targetSubElement, int timeout);
        IList<ITestableWebElement> FindSubElements(Types.Alias subElementProperties);
        IList<ITestableWebElement> FindSubElements(Types.Alias subElementProperties, int timeout);
        
        void RegisterSubElement(string name, dynamic elementProperties);
        void RegisterSubElement(string name, Types.Alias elementProperties);
    }
}
