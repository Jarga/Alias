using Aliases.Common;
using Aliases.Common.Configuration;
using Aliases.Common.Shared.Types;
using Aliases.Example.Application.Google.Pages.SearchResults;

namespace Aliases.Example.Application.Google.Pages
{
    public class GoogleBasePage : WebPage
    {
        public Alias QueryBox = new Alias() { Name = "q" };
        public Alias SubmitQuery = new Alias() { Css = "[name=btnK], [name=btnG]" };

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
