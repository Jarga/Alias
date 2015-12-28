using Alias.Common;
using Alias.Common.Initialization.Interfaces;

namespace Alias.Example.Application.Google.Pages
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
            switch (TestConfiguration.TestEnvironmentType)
            {
                default:
                    return "https://www.google.com/";
            }
        }
    }
}
