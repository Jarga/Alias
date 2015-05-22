using TestAutomation.Applications.MarketOnce.Pages;
using TestAutomation.Initialization;
using TestAutomation.Shared;
using TestAutomation.Shared.Enumerations;

namespace TestAutomation.Applications.MarketOnce
{
    public class MarketOnceSession : WebPage
    {
        public static Environments Environment { get; set; }

        public static Sites Site { get { return Sites.MarketOnce; } }

        public MarketOnceSession() : base(GlobalTestSettings.BaseTestPageType)
        {
            Environment = GlobalTestSettings.TestEnvironment;
        }

        public MarketOnceSession(ITestableWebPage baseObject) : base(baseObject) { }

        public Login Open()
        {
            Open(GetEnvironmentUrl());
            Maximize();

            return new Login(BaseObject);
        }

        private static string GetEnvironmentUrl()
        {
            switch (Environment)
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
