using AutomationCore;
using AutomationCore.Initialization;
using AutomationCore.Shared;
using AutomationCore.Shared.Enumerations;
using TestAutomation.Applications.ClearVoiceSurveys.Pages;

namespace TestAutomation.Applications.ClearVoiceManage
{
    public class ClearVoiceManageSession : WebPage
    {
        public static Environments Environment { get; set; }

        public static Sites Site { get { return Sites.ClearVoiceSurveys; } }

        public ClearVoiceManageSession() : base(GlobalTestSettings.BaseTestPageType)
        {
            Environment = GlobalTestSettings.TestEnvironment;
        }

        public ClearVoiceManageSession(ITestableWebPage baseObject) : base(baseObject){}

        public FrontPage Open()
        {
            Open(GetEnvironmentUrl());
            Maximize();

            return new FrontPage(BaseObject);
        }

        private static string GetEnvironmentUrl()
        {
            switch (Environment)
            {
                case Environments.DEV:
                    return "http://dev.clearvoicesurveys.com/";
                case Environments.PRODUCTION:
                    return "http://www.clearvoicesurveys.com/";
                default:
                    return "http://dev.clearvoicesurveys.com/";
            }
        }
    }
}
