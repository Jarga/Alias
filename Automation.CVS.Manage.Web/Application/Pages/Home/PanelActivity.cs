using Automation.Common.Initialization.Interfaces;

namespace Automation.CVS.Manage.Web.Application.Pages.Home
{
    public class PanelActivity : HomeTabBasePage
    {
        public PanelActivity(ITestConfiguration testConfig) : base(testConfig)
        {
            RegisterSubElement("New Panelist Chart", new { Id = "flashcontent_ctl00_cphMain_chartNewPanelists" });

            EnsureElementLoaded("New Panelist Chart", "Panel Activity loaded.", "Panel Activity failed to load.");
        }
    }
}
