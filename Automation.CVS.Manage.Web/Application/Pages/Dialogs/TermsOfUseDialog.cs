using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common;
using Automation.Common.Initialization.Interfaces;

namespace Automation.CVS.Manage.Web.Application.Pages.Dialogs
{
    public class TermsOfUseDialog : WebPage
    {
        public TermsOfUseDialog(ITestConfiguration testConfig) : base(testConfig)
        {
            SetActiveWindow("Pages/TermsOfUse.htm");
            //Must use contains because element has strange characters
            RegisterSubElement("Terms of Use Element", new { Text = "contains=Terms of Use" });

            EnsureElementLoaded("Terms of Use Element", "Terms of Use window opened.", "Terms of Use window failed to opened.");
        }
    }
}
