using Automation.Common.Shared.Enumerations;
using Automation.Common.XUnit.TestClasses;
using Automation.Common.XUnit.TraitAttributes;
using Automation.CVS.Manage.Web.Application;
using Automation.CVS.Manage.Web.Application.Pages.Home;
using Automation.CVS.Manage.Web.Application.Pages.Home.MyAccount;
using Automation.DataAccess;
using Automation.DataAccess.Repositories.Interfaces.Admin;
using Automation.DataAccess.Repositories.Keys.Admin;
using Automation.DataAccess.Repositories.Models.Admin;
using Xunit;
using Xunit.Abstractions;

namespace Automation.CVS.Manage.Web.Tests.Smoke
{
    public class HomeTests : BasicXUnitTests
    {
        public HomeTests(ITestOutputHelper output) : base(output){}

        [Fact]
        [CustomTrait("Suite", "Smoke")]
        public void Navigate_All_Home_Pages()
        {
            User user = RepositoryProvider.Get<IUserRepository>().Get(new UserKey() { Site = Sites.ClearVoiceManage.ToString() });

            var updateAccountPage = new ManageSession(TestConfiguration)
                                .Open()
                                .LogIn(user.UserName, user.Password)
                                .NavigateMenu<PanelMetrics>("Panel Metrics")
                                .NavigateMenu<CustomMetrics>("Custom Metrics")
                                .NavigateMenu<PanelActivity>("Panel Activity")
                                .NavigateMenu<SurveyActivity>("Survey Activity")
                                .NavigateMenu<MyAccount>("My Account")
                                .NavigateMenu<EditAccount>("My Account", "Update Account Info")
                                .NavigateMenu<ChangePassword>("My Account", "Change Password")
                                .NavigateMenu<ChangeEmail>("My Account", "Change Email");

            Assert.True(updateAccountPage != null, "Failed to navigate home tab!");
        }
    }
}
