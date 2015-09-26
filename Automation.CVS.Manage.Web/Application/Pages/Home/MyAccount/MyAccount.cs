using Automation.Common.Initialization.Interfaces;

namespace Automation.CVS.Manage.Web.Application.Pages.Home.MyAccount
{
    public class MyAccount : HomeTabBasePage
    {
        public MyAccount(ITestConfiguration testConfig) : base(testConfig)
        {
            RegisterSubElement("Update Account Information Link", new { Id = "ctl00_cphMain_lnkUpdate" });

            EnsureElementLoaded("Update Account Information Link", "My Account index loaded.", "My Account index failed to load.");
        }
    }
}
