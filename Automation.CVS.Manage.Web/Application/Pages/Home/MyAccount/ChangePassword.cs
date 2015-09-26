using Automation.Common.Initialization.Interfaces;

namespace Automation.CVS.Manage.Web.Application.Pages.Home.MyAccount
{
    public class ChangePassword : HomeTabBasePage
    {
        public ChangePassword(ITestConfiguration testConfig) : base(testConfig)
        {
            RegisterSubElement("Current Password Input", new { Id = "ctl00_cphMain_txtCurrentPassword" });

            EnsureElementLoaded("Current Password Input", "Change Password loaded.", "Change Password failed to load.");
        }
    }
}
