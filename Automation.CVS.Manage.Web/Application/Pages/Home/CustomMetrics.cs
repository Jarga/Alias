using Automation.Common.Initialization.Interfaces;

namespace Automation.CVS.Manage.Web.Application.Pages.Home
{
    public class CustomMetrics : HomeTabBasePage
    {
        public CustomMetrics(ITestConfiguration testConfig) : base(testConfig)
        {
            RegisterSubElement("No Metrics Div", new { Id = "ctl00_cphMain_cpCustomDemographics_divNoMetrics" });

            EnsureElementLoaded("No Metrics Div", "Custom Metrics loaded.", "Custom Metrics failed to load.");
        }
    }
}
