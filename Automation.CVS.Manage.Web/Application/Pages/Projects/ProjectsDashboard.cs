using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common.Initialization.Interfaces;

namespace Automation.CVS.Manage.Web.Application.Pages.Projects
{
    public class ProjectsDashboard : ProjectsTabBasePage
    {
        public ProjectsDashboard(ITestConfiguration testConfig) : base(testConfig)
        {
            RegisterSubElement("Open Projects By PM", new { Id = "ctl00_cphMain_ucOpenProjectsByPM_divActivityLoad" });

            EnsureElementLoaded("Open Projects By PM", "Projects Dashboard loaded.", "Projects Dashboard failed to load.");
        }
    }
}
