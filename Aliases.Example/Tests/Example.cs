using Aliases.Drivers.Selenium.Configuration;
using Aliases.Example.Application.Google.Pages;
using Aliases.TestExecutors.XUnit.TestClasses;
using Aliases.TestExecutors.XUnit.TraitAttributes;
using Xunit;
using Xunit.Abstractions;

namespace Aliases.Example.Tests
{
    public class Example : BasicXUnitTests<SeleniumTestConfiguration>
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
