using AutomationCore.TestClasses;
using TestAutomation.Applications.MarketOnce;
using TestAutomation.Applications.MarketOnce.Pages.List;
using Xunit;
using Xunit.Abstractions;

namespace TestAutomation.FunctionalTests.MarketOnce.Lists.CreateTarget
{
    public class CreateTargetTests : BasicTests
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
