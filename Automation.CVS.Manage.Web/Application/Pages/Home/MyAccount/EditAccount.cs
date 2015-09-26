using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common.Initialization.Interfaces;

namespace Automation.CVS.Manage.Web.Application.Pages.Home.MyAccount
{
    public class EditAccount : HomeTabBasePage
    {
        public EditAccount(ITestConfiguration testConfig) : base(testConfig)
        {
            RegisterSubElement("First Name Input", new { Id = "ctl00_cphMain_txtName" });
            RegisterSubElement("Last Name Input", new { Id = "ctl00_cphMain_txtLastName" });

            EnsureElementLoaded("First Name Input", "Edit Account loaded.", "Edit Account failed to load.");
        }
    }
}
