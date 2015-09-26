using Automation.Common;
using Automation.Common.Initialization.Interfaces;
using Automation.Common.Shared.Enumerations;
using Automation.MarketOnce.Web.Application.Pages;

namespace Automation.MarketOnce.Web.Application
{
    public class MarketOnceSession : WebPage
    {
        public Sites Site => Sites.MarketOnce;
        
        public MarketOnceSession(ITestConfiguration testConfig) : base(testConfig){}
        
        public Login Open()
        {
            Open(GetEnvironmentUrl());
            Maximize();

            return New<Login>();
        }

        private string GetEnvironmentUrl()
        {
            switch (TestConfiguration.TestEnvironmentType)
            {
                case EnvironmentType.Dev:
                    return "";
                case EnvironmentType.QA:
                    return "";
                case EnvironmentType.Production:
                    return "";
                default:
                    return "";
            }
        }
    }
}
