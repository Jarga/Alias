using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common.Initialization.Interfaces;
using Automation.Common.Shared.Types;

namespace Automation.PageObject.Example.Application.Google.Pages.SearchResults
{
    public class WebSearchResults : GoogleBasePage
    {
        Alias SubmitQuery = new Alias() { Name = "btnG" };
        Alias SelectedAllTab = new Alias() { TagName = "div", Text = "All", Class = "contains=hdtb-msel" };

        public WebSearchResults(ITestConfiguration testConfig) : base(testConfig)
        {
            EnsureElementLoaded(SelectedAllTab, "Google web search page loaded.", "Google web search page failed to load.");
        }
    }
}
