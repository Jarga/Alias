using Automation.Common.Executors.XUnit.TestClasses;
using Automation.Common.Executors.XUnit.TraitAttributes;
using Automation.PageObject.Example.Application.Google.Pages;
using Xunit;
using Xunit.Abstractions;

namespace Automation.PageObject.Example.Tests
{
    public class Example : BasicXUnitTests
    {
        public Example(ITestOutputHelper output) : base(output){}

        [Fact]
        [CustomTrait("Suite", "Example")]
        public void Search_Google()
        {
            var webSearch = new GoogleSession(TestConfiguration)
                            .Open()
                            .Search("Testing");

            Assert.NotNull(webSearch);
        }

    }
}
