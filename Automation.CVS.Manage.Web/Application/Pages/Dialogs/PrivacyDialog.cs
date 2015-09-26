using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common;
using Automation.Common.Initialization.Interfaces;

namespace Automation.CVS.Manage.Web.Application.Pages.Dialogs
{
    public class PrivacyDialog : WebPage
    {
        public PrivacyDialog(ITestConfiguration testConfig) : base(testConfig)
        {
            SetActiveWindow("Pages/Privacy.htm");
            RegisterSubElement("Privacy Policy Title", new { Tag = "span", Text = "Privacy Policy"});

            EnsureElementLoaded("Privacy Policy Title", "Privacy policy window opened.", "Privacy policy window failed to opened.");
        }
    }
}
