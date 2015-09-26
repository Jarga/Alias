using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common;
using Automation.Common.Initialization.Interfaces;

namespace Automation.CVS.Manage.Web.Application.Pages.Home
{
    public class PanelMetrics : HomeTabBasePage
    {
        public PanelMetrics(ITestConfiguration testConfig) : base(testConfig)
        {
            RegisterSubElement("Country Table", new { Id = "ctl00_cphMain_cellCountry" });

            EnsureElementLoaded("Country Table", "Panel Metrics loaded.", "Panel Metrics failed to load.");
        }
    }
}
