using Alias.Common.Initialization.Interfaces;

namespace Alias.Example.Application.Google.Pages.SearchResults
{
    public class WebSearchResults : GoogleBasePage
    {
        Alias.Common.Shared.Types.Alias SubmitQuery = new Alias.Common.Shared.Types.Alias() { Name = "btnG" };
        Alias.Common.Shared.Types.Alias SelectedAllTab = new Alias.Common.Shared.Types.Alias() { TagName = "div", Text = "All", Class = "contains=hdtb-msel" };

        public WebSearchResults(ITestConfiguration testConfig) : base(testConfig)
        {
            EnsureElementLoaded(SelectedAllTab, "Google web search page loaded.", "Google web search page failed to load.");
        }
    }
}
