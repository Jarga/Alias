using Automation.Common;
using Automation.Common.Initialization.Interfaces;
using Automation.Common.Shared.Enumerations;
using Automation.CVS.Manage.Web.Application.Pages;

namespace Automation.CVS.Manage.Web.Application
{
    public class ManageSession : WebPage
    {
        public Sites Site => Sites.ClearVoiceManage;

        public ManageSession(ITestConfiguration baseObject) : base(baseObject){ }

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
