using System;
using System.Collections.Generic;
using AutomationCore.TestClasses;
using TestAutomation.Applications.MarketOnce;
using Xunit;
using Xunit.Abstractions;

namespace TestAutomation.FunctionalTests.MarketOnce.Admin.Users
{
    public class CreateUserTests : BasicXUnitTests
    {
        public CreateUserTests(ITestOutputHelper output) : base(output){}

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
    }
}
