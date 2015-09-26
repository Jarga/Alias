using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common.Initialization.Interfaces;
using Automation.CVS.Manage.Web.Application.Pages.Reports;

namespace Automation.CVS.Manage.Web.Application.Pages.Projects
{
    /// <summary>
    /// Extends Reports Main Page because it has the same base as the reports tab pages but is in the project section for some reason
    /// </summary>
    public class PanelPayments : ReportsTabBasePage
    {
        public PanelPayments(ITestConfiguration testConfig) : base(testConfig)
        {
            RegisterSubElement("Panel Payments Title", new { Id = "ctl00_cphMain_pnlPanels_lblHeaderText", Text = "contains=Panel Payments" });

            EnsureElementLoaded("Panel Payments Title", "Panel Payments loaded.", "Panel Payments failed to load.");
        }
    }
}
