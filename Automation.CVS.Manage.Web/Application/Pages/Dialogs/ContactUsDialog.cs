using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common;
using Automation.Common.Initialization.Interfaces;

namespace Automation.CVS.Manage.Web.Application.Pages.Dialogs
{
    public class ContactUsDialog : WebPage
    {
        public ContactUsDialog(ITestConfiguration testConfig) : base(testConfig)
        {
            SetActiveWindow("Pages/ContactUs.aspx");
            RegisterSubElement("Contact Us Header", new { @class = "formTitle", Text = "contains=Contact Clear Voice Research" });

            EnsureElementLoaded("Contact Us Header", "Contact Us window opened.", "Contact Us window failed to opened.");
        }
    }
}
