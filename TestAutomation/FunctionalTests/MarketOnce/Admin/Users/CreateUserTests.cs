using System;
using System.Collections.Generic;
using Automation.DataAccess;
using Automation.DataAccess.Repositories.Interfaces.Admin;
using Automation.DataAccess.Repositories.Models.Admin;
using AutomationCore.XUnit.TestClasses;
using AutomationCore.XUnit.TraitAttributes;
using TestAutomation.Applications.MarketOnce;
using Xunit;
using Xunit.Abstractions;

namespace TestAutomation.FunctionalTests.MarketOnce.Admin.Users
{
    public class CreateUserTests : BasicXUnitTests
    {
        public CreateUserTests(ITestOutputHelper output) : base(output) { }
        
        [Fact]
        [CustomTrait("Suite", "Smoke;CreateUser;Nothing")] //Just an example, will need work
        public void Create_User()
        {
            User user = RepositoryProvider.Get<IUserRepository>().Get("sean.mcadams@oceansideten.com");

            var newUserName = "Test_User_" + DateTime.Now.Ticks;
            var email = newUserName + "@oceansideten.com";
            var password =  "P" + Guid.NewGuid().ToString("N") + "W";
            var newUserOrgs = new[]
            {
                new[] {"MarketOnce", "Jacksonville Test"},
                new[] {"MarketOnce", "Daniel Enterprise Org"}
            };
            var newUserRoles = new[] {"MarketOnce Administrator"};


            var userPage = new MarketOnceSession()
                            .Open()
                            .LogIn(user.UserName, user.Password)
                            .NavigateToUserAdmin()
                            .NavigateToCreateUser()
                            .CreateUser(email, newUserName, "AutomationScript", password, newUserOrgs, newUserRoles);

            Assert.True(userPage.UserExists(email), "Failed to create user!");

            var welcomePage = userPage.SwitchUser(email, password);

            //TODO: verify user has assigned orgs in org picker
            Assert.True(welcomePage.Exists("Logout"), "Unable to login as new user!");
        }
    }
    
}
