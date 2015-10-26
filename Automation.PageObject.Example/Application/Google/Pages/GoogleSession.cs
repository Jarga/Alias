using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common;
using Automation.Common.Initialization.Interfaces;
using Automation.Common.Shared.Enumerations;

namespace Automation.PageObject.Example.Application.Google.Pages
{
    public class GoogleSession : WebPage
    {
        public GoogleSession(ITestConfiguration testConfig) : base(testConfig){}

        public GoogleHomePage Open()
        {
            Open(GetEnvironmentUrl());
            Maximize();

            return New<GoogleHomePage>();
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
