using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common.Initialization.Interfaces;

namespace Automation.CVS.Manage.Web.Application.Pages.Projects
{
    public class InvoiceProjects : ProjectsTabBasePage
    {
        public InvoiceProjects(ITestConfiguration testConfig) : base(testConfig)
        {
            RegisterSubElement("Projects Ready To Invoice Title", new { @class = "headerTitle", Text = "contains=Projects Ready to Invoice" });

            EnsureElementLoaded("Projects Ready To Invoice Title", "Invoice Projects loaded.", "Invoice Projects failed to load.");
        }
    }
}
