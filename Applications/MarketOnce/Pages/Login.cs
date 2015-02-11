using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomation.Applications.MarketOnce.Pages.Admin;
using TestAutomation.Shared;

namespace TestAutomation.Applications.MarketOnce.Pages
{
    public class Login : WebPage
    {
        public Login(ITestableWebPage baseObject)
            : base(baseObject)
        {
            RegisterSubElement("Email", new { TagName = "input", Type = "text", Id = "UserName" });
            RegisterSubElement("Password", new { TagName = "input", Type = "password", Id = "Password"});
            RegisterSubElement("Log In", new { TagName = "input", Type = "image", Id = "ucLogin_Login1_LoginButton" });
        }

        public Welcome LogIn(string email, string password)
        {
            var emailElement = FindSubElement("Email");
            emailElement.Clear();
            emailElement.Type(email);

            Type("Password", password);
            Click("Log In");
            
            return new Welcome(BaseObject);;
        }
    }
}
