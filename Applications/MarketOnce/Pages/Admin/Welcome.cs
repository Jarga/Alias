using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomation.Applications.MarketOnce.Pages.Home;
using TestAutomation.Shared;

namespace TestAutomation.Applications.MarketOnce.Pages.Admin
{
    public class Welcome : MarketOnceBasePage
    {
        public Welcome(ITestableWebPage baseObject) : base(baseObject)
        {
            RegisterSubElement("Email Dashboard", new { Text = "Email", Id = "contains=ctl00_cphMain_dlModules" });
            RegisterSubElement("Users Link", new { Text = "Users", Id = "contains=marketOnceSiteMenu_ssmAdmin_childItems" });
            
            FindSubElement("Logout", 120);
        }

        public EmailDashboard NavigateToEmailModule()
        {
            Click("Email Dashboard");
            
            return new EmailDashboard(BaseObject);
        }

        public Users NavigateToUserAdmin()
        {
            Click("Users Link");

            return new Users(BaseObject);
        }
    }
}
