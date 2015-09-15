using AutomationCore;
using AutomationCore.Initialization;
using AutomationCore.Output;
using AutomationCore.Shared;
using AutomationCore.Shared.Enumerations;
using TestAutomation.Applications.MarketOnce.Pages;

namespace TestAutomation.Applications.MarketOnce
{
    public class MarketOnceSession : WebPage
    {

        public static Sites Site { get { return Sites.MarketOnce; } }

        public MarketOnceSession() : base(Global.BaseTestPageType)
        {
        }

        public MarketOnceSession(ITestableWebPage baseObject): base(baseObject){}

        public Login Open()
        {
            Open(GetEnvironmentUrl());
            Maximize();

            return New<Login>();
        }

        private static string GetEnvironmentUrl()
        {
            switch (Global.TestEnvironment)
            {
                case Environments.DEV:
                    return "http://dev.marketonce.com/";
                case Environments.QA:
                    return "http://qa.marketonce.com/";
                case Environments.PRODUCTION:
                    return "http://app.marketonce.com/";
                default:
                    return "http://dev.marketonce.com/";
            }
        }
    }
}
