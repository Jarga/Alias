using Aliases.Common.Configuration;
using Aliases.Common.Shared.Types;

namespace Aliases.Example.Application.Google.Pages.SearchResults
{
    public class WebSearchResults : GoogleBasePage
    {
        public Alias SubmitQuery = new Alias() { Name = "btnG" };
        public Alias SelectedAllTab = new Alias() { TagName = "div", Text = "All" };

        public WebSearchResults(ITestConfiguration testConfig) : base(testConfig)
        {
            EnsureElementLoaded(SelectedAllTab, "Google web search page loaded.", "Google web search page failed to load.");
        }
    }
}
