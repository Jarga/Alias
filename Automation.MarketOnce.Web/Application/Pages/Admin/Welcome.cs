using Automation.Common.Initialization;
using Automation.Common.Shared;
using Automation.MarketOnce.Web.Application.Pages.Home;

namespace Automation.MarketOnce.Web.Application.Pages.Admin
{
    public class Welcome : BasePage
    {
        public Welcome(ITestableWebPage baseObject) : base(baseObject)
        {
            RegisterSubElement("Email Dashboard", new { Text = "Email", Id = "contains=ctl00_cphMain_dlModules" });
            RegisterSubElement("Users Link", new { Text = "Users", Id = "contains=marketOnceSiteMenu_ssmAdmin_childItems" });

            EnsurePageLoaded("Logout", "Opened Welcome Page", "Failed to open Welcome Page", true);
        }

        public EmailDashboard NavigateToEmailModule()
        {
            Click("Email Dashboard");

            return New<EmailDashboard>();
        }

        public Users NavigateToUserAdmin()
        {
            Click("Users Link");

            return New<Users>();
        }
    }
}
