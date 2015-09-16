using Automation.Common.XUnit.TestClasses;
using Automation.MarketOnce.Web.Application;
using Automation.MarketOnce.Web.Application.Pages.List;
using Xunit;
using Xunit.Abstractions;

namespace Automation.MarketOnce.Web.Tests.Lists.CreateTarget
{
    public class CreateTargetTests : BasicXUnitTests
    {
        public CreateTargetTests(ITestOutputHelper output) : base(output){}

        [Fact]
        [Trait("Suite", "EditTarget")]
        public void Create_Attribute_Target()
        {
            EditTarget targetPage = new MarketOnceSession()
                                        .Open()
                                        .LogIn("", "")
                                        .NavigateToEmailModule()
                                        .NavigateMenu<EditTarget>("Lists", "Create Target");

            Assert.NotNull(targetPage);
            Assert.True(targetPage.NewTarget("Test Target", "New List"), "Unable to see target expression canvas.");

        }

    }
}
