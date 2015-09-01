using AutomationCore;
using AutomationCore.Shared;
using TestAutomation.Applications.MarketOnce.Pages.Admin;

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

            //For some reason chrome does not want to type the first time you hit the site...
            emailElement.Clear();
            emailElement.Type("Empty");

            emailElement.Clear();
            emailElement.Type(email);

            Type("Password", password);

            Click("Log In");

            return New<Welcome>();
        }
    }
}
