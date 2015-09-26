using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common;
using Automation.Common.Initialization.Interfaces;

namespace Automation.CVS.Manage.Web.Application.Pages.Dialogs
{
    public class AboutUsDialog : WebPage
    {
        public AboutUsDialog(ITestConfiguration testConfig) : base(testConfig)
        {
            SetActiveWindow("Pages/AboutUs.aspx");
            RegisterSubElement("About Us Header", new { @class= "headerTitle", Text = "contains=About Clear Voice Research" });

            EnsureElementLoaded("About Us Header", "About Us window opened.", "About Us window failed to opened.");
        }
    }
}
