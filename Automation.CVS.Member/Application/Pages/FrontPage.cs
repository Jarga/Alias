using Automation.Common;
using Automation.Common.Shared;

namespace Automation.CVS.Member.Application.Pages
{
    public class FrontPage : WebPage
    {
        public FrontPage(ITestableWebPage baseObject) : base(baseObject)
        {
            RegisterSubElement("Email", new { TagName = "input", Type = "text", Id = "ctl01_ucHeader_ucLogin_txtLogin" });
            RegisterSubElement("Password", new { TagName = "input", Type = "text", Id = "ctl01_ucHeader_ucLogin_txtPassword"});
            RegisterSubElement("Log In", new { TagName = "input", Type = "submit", Id = "ctl01_ucHeader_ucLogin_btnLogin"});
        }

        public Dashboard LogIn(string email, string password)
        {
            Type("Email", email);
            Type("Password", password);
            Click("Log In");

            Dashboard dashboard = new Dashboard(BaseObject);

            dashboard.FindSubElement("Sign Out", 120);

            return dashboard;
        }
    }
}
