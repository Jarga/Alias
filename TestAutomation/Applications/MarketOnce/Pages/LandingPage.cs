using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationCore;
using AutomationCore.Shared;

namespace TestAutomation.Applications.MarketOnce.Pages
{
    public class LandingPage : WebPage
    {
        public LandingPage(ITestableWebPage baseObject) : base(baseObject)
        {
            RegisterSubElement("Username", new { id = "header_login_form"});

            FindSubElement("Username", 120);
        }
    }
}
