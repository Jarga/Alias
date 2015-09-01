using System;
using System.Collections.Generic;
using System.Linq;
using AutomationCore.Shared;
using AutomationCore.Shared.Exceptions;
using AutomationCore.Shared.Extensions;
using TestAutomation.Applications.MarketOnce.Controls.Smart;

namespace TestAutomation.Applications.MarketOnce.Pages.Admin
{
    public class Users : MarketOnceBasePage
    {
        public Users(ITestableWebPage baseObject) : base(baseObject)
        {
            RegisterSubElement("Create User", new { id = "contains=lbCreateNew" });
            RegisterSubElement("Email Address", new { id = "contains=pSearchCriteria_txtEmailAddress" });
            RegisterSubElement("First Name", new { id = "contains=spSearchCriteria_txtFirstName" });
            RegisterSubElement("Last Name", new { id = "contains=spSearchCriteria_txtLastName" });
            RegisterSubElement("Search", new { id = "contains=spSearchCriteria_btnSearch" });

            RegisterSubElement("Users Grid", new { id = "contains=spSearchResults_resultsGrid" });
            RegisterSubElement("Records Count", new { parentelement = "Users Grid", css = "tr[name=PagerRow] td:first-child" });

            FindSubElement("Create User", 120);
        }

        public EditUser NavigateToCreateUser()
        {
            Click("Create User");

            return New<EditUser>();
        }

        public bool UserExists(string emailAddress = null, string firstName = null, string lastName = null)
        {
            if (string.IsNullOrWhiteSpace(emailAddress) && string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("At least one parameter must be populated to find a user!");
            }

            var searchCriteria = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(emailAddress))
            {
                CompleteTextInput("Email Address", emailAddress);
                searchCriteria.Add("Email Address", emailAddress);
            }

            if (!string.IsNullOrWhiteSpace(firstName))
            {
                CompleteTextInput("First Name", firstName);
                searchCriteria.Add("First Name", firstName);
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                CompleteTextInput("Last Name", lastName);
                searchCriteria.Add("Last Name", lastName);
            }

            var recordsTextBefore = FindSubElement("Records Count").InnerHtml();

            FindSubElement("Search").Click();

            if (!Wait.For(() => !FindSubElement("Records Count").InnerHtml().Equals(recordsTextBefore), 60, 1))
            {
                throw new ActionFailedException("User grid search failed!");
            }

            SmartGrid grid = new SmartGrid(BaseObject, "spSearchResults_resultsGrid");

            var matches = grid.FindRows(searchCriteria);

            return matches.Any();
        }
    }
}
