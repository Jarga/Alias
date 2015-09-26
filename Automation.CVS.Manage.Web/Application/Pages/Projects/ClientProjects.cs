using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common.Initialization.Interfaces;

namespace Automation.CVS.Manage.Web.Application.Pages.Projects
{
    public class ClientProjects : ProjectsTabBasePage
    {
        public ClientProjects(ITestConfiguration testConfig) : base(testConfig)
        {
            RegisterSubElement("Client Projects Title", new { Id = "ctl00_pageTitle", Text = "Client Projects" });

            EnsureElementLoaded("Client Projects Title", "Client Projects loaded.", "Client Projects failed to load.");
        }
    }
}
