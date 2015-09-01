using AutomationCore.Shared;

namespace TestAutomation.Applications.MarketOnce.Pages.Home
{
    public class EmailDashboard : MarketOnceBasePage
    {
        public EmailDashboard(ITestableWebPage baseObject) : base(baseObject)
        {
            RegisterSubElement("Dashboard Panel", new { Id = "ctl00_ucTitleBar_lblTitlebarHeader", Text = "contains=Dashboard" });

            FindSubElement("Dashboard Panel", 120);
        }
    }
}
