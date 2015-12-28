using Alias.Common;
using Alias.Common.Initialization.Interfaces;
using Alias.Example.Application.Google.Pages.SearchResults;

namespace Alias.Example.Application.Google.Pages
{
    public class GoogleBasePage : WebPage
    {
        Alias.Common.Shared.Types.Alias QueryBox = new Alias.Common.Shared.Types.Alias() { Name = "q" };
        protected Alias.Common.Shared.Types.Alias SubmitQuery = new Alias.Common.Shared.Types.Alias() { Css = "[name=btnK], [name=btnG]" };

        public GoogleBasePage(ITestConfiguration testConfig) : base(testConfig)
        {
            EnsureElementLoaded(QueryBox, null, "Google page failed to load.");
        }
        public WebSearchResults Search(string value)
        {
            Type(QueryBox, value);
            Click(SubmitQuery);

            return New<WebSearchResults>();
        }
    }
}
