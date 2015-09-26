using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common;
using Automation.Common.Initialization.Interfaces;
using Automation.CVS.Manage.Web.Application.Pages.Dialogs;
using Automation.CVS.Manage.Web.Application.Pages.Home;

namespace Automation.CVS.Manage.Web.Application.Pages
{
    public class Login : WebPage
    {
        public Login(ITestConfiguration testConfig) : base(testConfig)
        {
            RegisterSubElement("Login", new { Id = "UcLogin1_Login1_UserName" });
            RegisterSubElement("Password", new { Id = "UcLogin1_Login1_Password" });
            RegisterSubElement("Login Button", new { Id = "UcLogin1_Login1_LoginButton" });

            RegisterSubElement("Privacy", new { Id = "SiteFooter_lbPrivacy" });
            RegisterSubElement("Terms of Use", new { Id = "SiteFooter_lbTermsOfUse" });
            RegisterSubElement("About Us", new { Id = "SiteFooter_lbAboutUs" });
            RegisterSubElement("Contact Us", new { Id = "SiteFooter_lbContactUs" });
            RegisterSubElement("Support", new { Id = "SiteFooter_lbSupport" });

            EnsureElementLoaded("Login", "Login page loaded.", "Failed to load login page.");
        }
        public Start LogIn(string email, string password)
        {
            Type("Login", email);
            Type("Password", password);
            Click("Login Button");
            
            return New<Start>();
        }

        public PrivacyDialog OpenPrivacy()
        {
            Click("Privacy");

            return New<PrivacyDialog>();
        }

        public TermsOfUseDialog OpenTermsOfUse()
        {
            Click("Terms of Use");

            return New<TermsOfUseDialog>();
        }

        public AboutUsDialog OpenAboutUs()
        {
            Click("About Us");

            return New<AboutUsDialog>();
        }

        public ContactUsDialog OpenContactUs()
        {
            Click("Contact Us");

            return New<ContactUsDialog>();
        }

        public SupportDialog OpenSupport()
        {
            Click("Support");

            return New<SupportDialog>();
        }
    }
}
