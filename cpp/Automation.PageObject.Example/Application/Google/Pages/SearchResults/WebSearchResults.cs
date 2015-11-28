using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common.Initialization.Interfaces;

namespace Automation.PageObject.Example.Application.Google.Pages.SearchResults
{
    public class WebSearchResults : GoogleBasePage
    {
        public WebSearchResults(ITestConfiguration testConfig) : base(testConfig)
        {
            RegisterSubElement("Submit Query", new { name = "btnG" });
            RegisterSubElement("Selected Web Tab", new { TagName = "div", Text = "Web", @class = "contains=hdtb-msel" });

            EnsureElementLoaded("Selected Web Tab", "Google web search page loaded.", "Google web search page failed to load.");
        }
    }
}
