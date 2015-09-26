using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common.Initialization.Interfaces;

namespace Automation.CVS.Manage.Web.Application.Pages.Projects
{
    public class EditProject : ProjectsTabBasePage
    {
        public EditProject(ITestConfiguration testConfig) : base(testConfig)
        {
            RegisterSubElement("Edit Project Panel", new { Id = "ctl00_cphMain_cpEditProject" });


            RegisterSubElement("Project Details Tab", new { Id = "ctl00_cphMain_cpEditProject_ucProjectDetails_tabProjectDetails" });
            RegisterSubElement("Project Details Content", new { Id = "ctl00_cphMain_cpEditProject_ucProjectDetails_cpEditProject" });

            RegisterSubElement("Project Notes Tab", new { Id = "ctl00_cphMain_cpEditProject_ucProjectDetails_tabProjectNotes" });
            RegisterSubElement("Project Notes Content", new { Id = "ctl00_cphMain_cpEditProject_ucProjectDetails_cpProjectNotes" });

            RegisterSubElement("Project Triggers Tab", new { Id = "ctl00_cphMain_cpEditProject_ucProjectDetails_tabProjectTriggers" });
            RegisterSubElement("Project Triggers Content", new { Id = "ctl00_cphMain_cpEditProject_ucProjectDetails_cpProjectTriggers" });

            RegisterSubElement("Survey Ready Content Tab", new { Id = "ctl00_cphMain_cpEditProject_ucProjectDetails_tabProjectSurveyReadyContent" });
            RegisterSubElement("Survey Ready Content", new { Id = "ctl00_cphMain_cpEditProject_ucProjectDetails_cpProjectSurveyReadyContent" });

            EnsureElementLoaded("Edit Project Panel", "Edit Project loaded.", "Edit Project failed to load.");
        }

        public EditProject OpenProjectDetailsTab()
        {
            Click("Project Details Tab");

            EnsureElementLoaded("Project Details Content", "Project Details tab loaded.", "Project Details tab failed to load");
            return this;
        }

        public EditProject OpenProjectNotesTab()
        {
            Click("Project Notes Tab");

            EnsureElementLoaded("Project Notes Content", "Project Notes tab loaded.", "Project Notes tab failed to load");
            return this;
        }

        public EditProject OpenProjectTriggersTab()
        {
            Click("Project Triggers Tab");

            EnsureElementLoaded("Project Triggers Content", "Project Triggers tab loaded.", "Project Triggers tab failed to load");
            return this;
        }

        public EditProject OpenSurveyReadyContentTab()
        {
            Click("Survey Ready Content Tab");

            EnsureElementLoaded("Survey Ready Content", "Survey Ready Content tab loaded.", "Survey Ready Content tab failed to load");
            return this;
        }
    }
}
