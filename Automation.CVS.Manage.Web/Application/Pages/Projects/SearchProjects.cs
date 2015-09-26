using Automation.Common.Initialization.Interfaces;

namespace Automation.CVS.Manage.Web.Application.Pages.Projects
{
    public class SearchProjects : ProjectsTabBasePage
    {
        public SearchProjects(ITestConfiguration testConfig) : base(testConfig)
        {
            RegisterSubElement("Search Projects Title", new { Id = "ctl00_pageTitle", Text = "Search Projects" });

            EnsureElementLoaded("Search Projects Title", "Search Projects loaded.", "Search Projects failed to load.");
        }
    }
}
