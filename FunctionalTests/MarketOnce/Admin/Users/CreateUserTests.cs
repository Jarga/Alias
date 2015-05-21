using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomation.Applications.MarketOnce;
using TestAutomation.Applications.MarketOnce.Pages.Admin;
using TestAutomation.Applications.MarketOnce.Pages.List;
using Xunit;
using Xunit.Abstractions;

namespace TestAutomation.FunctionalTests.MarketOnce.Admin.Users
{
    public class CreateUserTests
    {
        private readonly ITestOutputHelper output;

        public CreateUserTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        [Trait("Suite", "CreateUser")]
        public void Create_User()
        {
            var userCreated = new MarketOnceSession()
                .Open()
                .LogIn("", "")
                .NavigateToUserAdmin()
                .NavigateToCreateUser()
                .CreateUser("trest@gmail.com", "Test", "Stuff", "", new []{ new[]{ "MarketOnce", "Jacksonville Test" } }, new []{ "MarketOnce Administrator" } );
            Assert.True(userCreated, "Field to create user!");
        }
    }
}
