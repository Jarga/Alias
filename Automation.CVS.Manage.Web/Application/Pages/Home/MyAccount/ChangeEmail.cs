using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common.Initialization.Interfaces;

namespace Automation.CVS.Manage.Web.Application.Pages.Home.MyAccount
{
    public class ChangeEmail : HomeTabBasePage
    {
        public ChangeEmail(ITestConfiguration testConfig) : base(testConfig)
        {
            RegisterSubElement("Current Email Address", new { Id = "ctl00_cphMain_lblEmail" });

            EnsureElementLoaded("Current Email Address", "Change Email loaded.", "Change Email failed to load.");
        }
    }
}
