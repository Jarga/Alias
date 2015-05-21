using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomation.Shared;

namespace TestAutomation.Applications.MarketOnce.Pages.Admin
{
    public class Users : MarketOnceBasePage
    {
        public Users(ITestableWebPage baseObject) : base(baseObject)
        {
            RegisterSubElement("Create User", new { id = "contains=lbCreateNew" });

            FindSubElement("Create User", 120);
        }

        public EditUser NavigateToCreateUser()
        {
            Click("Create User");

            return new EditUser(BaseObject);
        }
    }
}
