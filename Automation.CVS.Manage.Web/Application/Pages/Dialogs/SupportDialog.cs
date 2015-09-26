using Automation.Common;
using Automation.Common.Initialization.Interfaces;

namespace Automation.CVS.Manage.Web.Application.Pages.Dialogs
{
    public class SupportDialog : WebPage
    {
        public SupportDialog(ITestConfiguration testConfig) : base(testConfig)
        {
            SetActiveWindow("support.clearvoicesurveys.com");
            RegisterSubElement("Submit a Ticket Link", new { @class = "toptoolbarlink", Text = "contains=Submit a Ticket" });

            EnsureElementLoaded("Submit a Ticket Link", "Support window opened.", "Support window failed to opened.");
        }
    }
}
