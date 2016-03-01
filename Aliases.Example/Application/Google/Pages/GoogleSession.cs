using Aliases.Common;
using Aliases.Common.Configuration;

namespace Aliases.Example.Application.Google.Pages
{
    public class GoogleSession : WebPage
    {
        public GoogleSession(ITestConfiguration testConfig) : base(testConfig){}

        public GoogleBasePage Open()
        {
            Open(GetEnvironmentUrl());
            Maximize();

            return New<GoogleBasePage>();
        }

        private string GetEnvironmentUrl()
        {
            if (string.IsNullOrWhiteSpace(TestConfiguration.BaseTestUrl))
            {
                switch (TestConfiguration.TestEnvironmentType)
                {
                    default:
                        return "https://www.google.com/";
                }
            }

            return TestConfiguration.BaseTestUrl;
        }
    }
}
