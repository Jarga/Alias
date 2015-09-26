using Automation.Common.Initialization.Interfaces;

namespace Automation.CVS.Manage.Web.Application.Pages.Home
{
    public class Start : HomeTabBasePage
    {
        public Start(ITestConfiguration testConfig) : base(testConfig)
        {
            RegisterSubElement("Panelists By Country Table", new { Id = "ctl00_cphMain_gvPanelByCountry" });

            EnsureElementLoaded("Panelists By Country Table", "Dashboard loaded.", "Dashboard failed to load.");
        }
    }
}
