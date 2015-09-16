using Automation.Common;
using Automation.Common.Shared;

namespace Automation.MarketOnce.Web.Application.Pages
{
    public class LandingPage : WebPage
    {
        public LandingPage(ITestableWebPage baseObject) : base(baseObject)
        {
            RegisterSubElement("Username", new { id = "header_form_login"});

            FindSubElement("Username", 120);
        }
    }
}
