using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common;
using Automation.Common.Initialization.Interfaces;
using Automation.Common.Shared.Types;
using Automation.PageObject.Example.Application.Google.Pages.SearchResults;

namespace Automation.PageObject.Example.Application.Google.Pages
{
    public class GoogleBasePage : WebPage
    {
        Alias QueryBox = new Alias() { Name = "q" };
        protected Alias SubmitQuery = new Alias() { Css = "[name=btnK], [name=btnG]" };

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
