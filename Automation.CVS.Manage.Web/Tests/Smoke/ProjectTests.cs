using System;
using Automation.Common.Shared.Enumerations;
using Automation.Common.XUnit.TestClasses;
using Automation.Common.XUnit.TraitAttributes;
using Automation.CVS.Manage.Web.Application;
using Automation.CVS.Manage.Web.Application.Pages.Home;
using Automation.CVS.Manage.Web.Application.Pages.Home.MyAccount;
using Automation.CVS.Manage.Web.Application.Pages.Projects;
using Automation.DataAccess;
using Automation.DataAccess.Repositories.Interfaces.Admin;
using Automation.DataAccess.Repositories.Keys.Admin;
using Automation.DataAccess.Repositories.Models.Admin;
using Xunit;
using Xunit.Abstractions;

namespace Automation.CVS.Manage.Web.Tests.Smoke
{
    public class ProjectTests : BasicXUnitTests
    {
        public ProjectTests(ITestOutputHelper output) : base(output){}

        [Fact]
        [CustomTrait("Suite", "Smoke")]
        public void Navigate_All_Project_Pages()
        {
            User user = RepositoryProvider.Get<IUserRepository>().Get(new UserKey() { Site = Sites.ClearVoiceManage.ToString() });

            var pollsCenter = new ManageSession(TestConfiguration)
                                .Open()
                                .LogIn(user.UserName, user.Password)
                                .NavigateToProjectsTab()
                                .NavigateMenu<ProjectsDashboard>("Dashboard")
                                .NavigateMenu<SearchProjects>("Search Projects")
                                .NavigateMenu<ClientProjects>("Client Projects")
                                .NavigateMenu<EditProject>("Create Project")
                                .OpenProjectNotesTab()
                                .OpenProjectTriggersTab()
                                .OpenProjectDetailsTab()
                                .OpenSurveyReadyContentTab()
                                .NavigateMenu<InvoiceProjects>("Invoice Projects")
                                .NavigateMenu<PanelPayments>("Panel Payments")
                                .NavigateToProjectsTab()
                                .NavigateMenu<PollCenter>("Poll Center")
                                .OpenMyPollsTab()
                                .OpenCreatePollsTab();

            Assert.True(pollsCenter != null, "Failed to navigate polls center page!");
        }
    }
}
