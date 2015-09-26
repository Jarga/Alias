using Automation.Common.Initialization;
using Automation.Common.Initialization.Interfaces;
using Automation.Common.Shared;

namespace Automation.MarketOnce.Web.Application.Pages.Home
{
    public class EmailDashboard : BasePage
    {
        public EmailDashboard(ITestConfiguration baseObject) : base(baseObject)
        {
            RegisterSubElement("Dashboard Panel", new { Id = "ctl00_ucTitleBar_lblTitlebarHeader", Text = "contains=Dashboard" });

            FindSubElement("Dashboard Panel", 120);
            TestConfiguration.TestOutput.WriteLineWithScreenshot("Opened Email Dashboard", GetScreenshot());
        }
    }
}
