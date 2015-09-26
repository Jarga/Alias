using Automation.Common.Shared.Enumerations;
using Automation.Common.XUnit.TestClasses;
using Automation.Common.XUnit.TraitAttributes;
using Automation.CVS.Manage.Web.Application;
using Automation.DataAccess;
using Automation.DataAccess.Repositories.Interfaces.Admin;
using Automation.DataAccess.Repositories.Keys.Admin;
using Automation.DataAccess.Repositories.Models.Admin;
using Xunit;
using Xunit.Abstractions;

namespace Automation.CVS.Manage.Web.Tests.Smoke
{
    public class GeneralTests : BasicXUnitTests
    {
        public GeneralTests(ITestOutputHelper output) : base(output){ }

        [Fact]
        [CustomTrait("Suite", "Smoke")]
        public void Navigate_Footer_Links()
        {
            var loginPage = new ManageSession(TestConfiguration).Open();

            loginPage.OpenPrivacy().Close();
            loginPage.OpenTermsOfUse().Close();
            loginPage.OpenAboutUs().Close();
            loginPage.OpenContactUs().Close();
            loginPage.OpenSupport().Close();

            Assert.True(loginPage.IsDisplayed(), "Failed to switch back to login page after opening footer links!");
        }

        [Fact]
        [CustomTrait("Suite", "Smoke")]
        public void Change_Org()
        {
            User user = RepositoryProvider.Get<IUserRepository>().Get(new UserKey() { Site = Sites.ClearVoiceManage.ToString() });

            var startPage = new ManageSession(TestConfiguration)
                                .Open()
                                .LogIn(user.UserName, user.Password)
                                .ChangeOrganization("Clear Voice Surveys");

            Assert.True(startPage != null, "Failed to load start page!");
        }
    }
}
