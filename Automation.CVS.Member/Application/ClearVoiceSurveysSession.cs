using Automation.Common;
using Automation.Common.Initialization;
using Automation.Common.Shared;
using Automation.Common.Shared.Enumerations;
using Automation.CVS.Member.Application.Pages;

namespace Automation.CVS.Member.Application
{
    public class ClearVoiceSurveysSession : WebPage
    {
        public static Sites Site { get { return Sites.ClearVoiceSurveys; } }

        public ClearVoiceSurveysSession() : base(Global.BaseTestPageType){}

        public ClearVoiceSurveysSession(ITestableWebPage baseObject) : base(baseObject){}

        public FrontPage Open()
        {
            Open(GetEnvironmentUrl());
            Maximize();

            return New<FrontPage>();
        }

        private static string GetEnvironmentUrl()
        {
            switch (Global.TestEnvironment)
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
