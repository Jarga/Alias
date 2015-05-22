using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomation.Applications.MarketOnce;
using TestAutomation.Applications.MarketOnce.Pages.Admin;
using TestAutomation.Applications.MarketOnce.Pages.List;
using TestAutomation.Initialization;
using TestAutomation.Shared.Enumerations;
using Xunit;
using Xunit.Abstractions;

namespace TestAutomation.FunctionalTests.MarketOnce.Admin.Users
{
    public class CreateUserTests : IDisposable
    {
        private readonly ITestOutputHelper output;

        public CreateUserTests(ITestOutputHelper output)
        {
            this.output = output;
            GlobalTestSettings.Initialize();
        }

        public static IEnumerable<object[]> GetCreateUserArgs()
        {
            return new List<object[]>()
            {
                new object[]
                {
                    "sean.mcadams@oceansideten.com", 
                    "", 
                    "", 
                    new[]
                    {
                        new[] { "MarketOnce", "Jacksonville Test" }
                    }, 
                    new[] { "MarketOnce Administrator" }
                },
                new object[]
                {
                    "sean.mcadams@oceansideten.com", 
                    "", 
                    "", 
                    new[]
                    {
                        new[] { "MarketOnce", "Jacksonville Test" }, 
                        new[] { "MarketOnce", "Daniel Enterprise Org" }
                    }, 
                    new[] { "MarketOnce Administrator" }
                }
            };
        }

        [Theory]
        [MemberData("GetCreateUserArgs")]
        [Trait("Suite", "CreateUser")]
        public void Create_User(string userName, string password, string newUserPassword, string [][] newUserOrgs, string [] newUserRoles)
        {
            var newUserName = "Test_User_" + DateTime.Now.Ticks;
            var email = newUserName + "@oceansideten.com";

            var userPage = new MarketOnceSession()
                            .Open()
                            .LogIn(userName, password)
                            .NavigateToUserAdmin()
                            .NavigateToCreateUser()
                            .CreateUser(email, newUserName, "AutomationScript", newUserPassword, newUserOrgs, newUserRoles);
            
            Assert.True(userPage.UserExists(email), "Field to create user!");
        }

        public void Dispose()
        {
            GlobalTestSettings.BaseTestPageType.Close();
        }
    }
}
