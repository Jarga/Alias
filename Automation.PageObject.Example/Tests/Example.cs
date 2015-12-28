using Alias.Common.Executors.XUnit.TestClasses;
using Alias.Common.Executors.XUnit.TraitAttributes;
using Alias.Example.Application.Google.Pages;
using Xunit;
using Xunit.Abstractions;

namespace Alias.Example.Tests
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
