using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common.Initialization.Interfaces;

namespace Automation.CVS.Manage.Web.Application.Pages.Projects
{
    public class PollCenter : ProjectsTabBasePage
    {
        public PollCenter(ITestConfiguration testConfig) : base(testConfig)
        {
            RegisterSubElement("Community Polls Title", new { Id = "ctl00_pageTitle", Text = "Community Polls" });
            
            RegisterSubElement("Create Polls Tab", new { Id = "ctl00_cphMain_btnCreateSurvey" });
            RegisterSubElement("Create Polls Main Text", new { Id = "ctl00_cphMain_ucCreateSurvey_pMainText" });

            RegisterSubElement("My Polls Tab", new { Id = "ctl00_cphMain_btnMySurveys" });
            RegisterSubElement("My Polls Text", new { Id = "ctl00_cphMain_ucMySurveys_pnlPanelUrls" });

            EnsureElementLoaded("Community Polls Title", "Community Polls loaded.", "Community Polls failed to load.");
        }

        public PollCenter OpenCreatePollsTab()
        {
            Click("Create Polls Tab");

            EnsureElementLoaded("Create Polls Main Text", "Create Polls tab loaded.", "Create Polls tab failed to load");
            return this;
        }

        public PollCenter OpenMyPollsTab()
        {
            Click("My Polls Tab");

            EnsureElementLoaded("My Polls Text", "My Polls tab loaded.", "My Polls tab failed to load");
            return this;
        }
    }
}
