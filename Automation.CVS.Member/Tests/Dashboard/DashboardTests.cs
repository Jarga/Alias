using System;
using System.Collections.Generic;
using Automation.CVS.Member.Application;
using Automation.CVS.Member.Application.Pages;
using Xunit;

namespace Automation.CVS.Member.Tests.Dashboard
{
    public class DashboardTests
    {
        [Fact]
        public void Navigate_Dashboard()
        {
            Tuple<string, string> login = new Tuple<string, string>("ddddd", "ddddd");
            List<string> tabsList = new List<string>() { "My Rewards Tab", "My Messages Tab", "My Achievements Tab", "My Account Tab", "My Sweepstakes Tab", "My Polls Tab" };


            FrontPage frontpage = new ClearVoiceSurveysSession().Open();

            foreach (string tab in tabsList)
            {
                Application.Pages.Dashboard dashboard = frontpage.LogIn(login.Item1, login.Item2);

                Assert.True(dashboard.GetCurrentUrl().Contains("/EmailDashboard.aspx"), "Expecting URL to contain 'EmailDashboard.aspx'!");

                dashboard.Click(tab);
                Assert.True(dashboard.Exists("Your EmailDashboard Title"), "Unable to see EmailDashboard!");

                frontpage = dashboard.SignOut();

                Assert.True(frontpage.GetCurrentUrl().Contains("/Default.aspx"), "Expecting URL to contain 'Default.aspx'!");
            }

            frontpage.Close();
        }

    }
}
