using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common;
using Automation.Common.Initialization.Interfaces;
using Automation.PageObject.Example.Application.Google.Pages.SearchResults;

namespace Automation.PageObject.Example.Application.Google.Pages
{
    public class GoogleBasePage : WebPage
    {
        public GoogleBasePage(ITestConfiguration testConfig) : base(testConfig)
        {
            RegisterSubElement("Query Box", new { name = "q" });
            RegisterSubElement("Submit Query", new { css = "[name=btnK], [name=btnG]" });

            EnsureElementLoaded("Query Box", null, "Google page failed to load.");
        }
    }
}
