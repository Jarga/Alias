using System.Collections.Generic;

namespace TestAutomation.Shared
{
    public interface ISearchableWebObject
    {
        ITestableWebElement FindSubElement(IDictionary<string, string> subElementProperties);
        ITestableWebElement FindSubElement(IDictionary<string, string> subElementProperties, int timeout);
        IList<ITestableWebElement> FindSubElements(IDictionary<string, string> subElementProperties);
    }
}
