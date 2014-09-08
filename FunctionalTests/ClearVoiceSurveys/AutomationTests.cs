using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAutomation.Applications.ClearVoiceSurveys;
using TestAutomation.Applications.ClearVoiceSurveys.Pages;
using TestAutomation.Shared;

namespace TestAutomation.FunctionalTests.ClearVoiceSurveys
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class AutomationTests
    {
        [TestMethod]
        public void Automation_Example_Real()
        {
            Tuple<string, string> login = new Tuple<string, string>("djoceansidex@gmail.com", "password");
            List<string> tabsList = new List<string>() { "My Rewards Tab", "My Messages Tab", "My Achievements Tab", "My Account Tab", "My Sweepstakes Tab", "My Polls Tab" };


            FrontPage frontpage = new ClearVoiceSurveysSession().Open();

            foreach (string tab in tabsList)
            {
                Dashboard dashboard = frontpage.LogIn(login.Item1, login.Item2);

                Assert.IsTrue(dashboard.GetCurrentUrl().Contains("/Dashboard.aspx"), "Expecting URL to contain 'Dashboard.aspx'!");

                dashboard.Click(tab);
                Assert.IsTrue(dashboard.Exists("Your Dashboard Title"), "Unable to see Dashboard!");

                frontpage = dashboard.SignOut();

                Assert.IsTrue(frontpage.GetCurrentUrl().Contains("/Default.aspx"), "Expecting URL to contain 'Default.aspx'!");
            }

            frontpage.Close();
        }

    }
}
