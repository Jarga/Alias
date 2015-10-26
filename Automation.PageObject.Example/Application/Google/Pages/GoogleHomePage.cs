using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common.Initialization.Interfaces;
using Automation.PageObject.Example.Application.Google.Pages.SearchResults;

namespace Automation.PageObject.Example.Application.Google.Pages
{
    public class GoogleHomePage : GoogleBasePage
    {
        public GoogleHomePage(ITestConfiguration testConfig) : base(testConfig)
        {
            EnsureElementLoaded("Submit Query", "Google home page loaded.", "Google home page failed to load.");
        }

        public WebSearchResults Search(string value)
        {
            Type("Query Box", value);
            Click("Submit Query");

            return New<WebSearchResults>();
        }
    }
}
