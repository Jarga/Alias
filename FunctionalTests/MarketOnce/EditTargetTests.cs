using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAutomation.Applications.MarketOnce;
using TestAutomation.Applications.MarketOnce.Pages;
using TestAutomation.Applications.MarketOnce.Pages.Admin;
using TestAutomation.Applications.MarketOnce.Pages.List;

namespace TestAutomation.FunctionalTests.MarketOnce
{
    public class EditTargetTests
    {
        [CodedUITest]
        public class AutomationTests
        {
            [TestMethod]
            public void Create_Attribute_Target()
            {
                EditTarget targetPage = new MarketOnceSession()
                                            .Open()
                                            .LogIn("", "")
                                            .NavigateToEmailModule()
                                            .NavigateMenu<EditTarget>("Lists", "Create Target");

                Assert.IsNotNull(targetPage);

            }

        }
    }
}
