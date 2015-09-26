using Automation.Common.Initialization.Interfaces;

namespace Automation.CVS.Manage.Web.Application.Pages.Home
{
    public class SurveyActivity : HomeTabBasePage
    {
        public SurveyActivity(ITestConfiguration testConfig) : base(testConfig)
        {
            RegisterSubElement("Survey Invitations Chart", new { Id = "flashcontent_ctl00_cphMain_chartSurveyInvitations" });

            EnsureElementLoaded("Survey Invitations Chart", "Survey Activity loaded.", "Survey Activity failed to load.");
        }
    }
}
