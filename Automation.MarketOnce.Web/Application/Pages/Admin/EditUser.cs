using System;
using System.Collections.Generic;
using System.Linq;
using Automation.Common.Initialization;
using Automation.Common.Initialization.Interfaces;
using Automation.Common.Shared;
using Automation.Common.Shared.Exceptions;
using Automation.Common.Shared.Extensions;
using Automation.MarketOnce.Web.Application.Controls.Ext;
using Automation.MarketOnce.Web.Application.Controls.Smart;

namespace Automation.MarketOnce.Web.Application.Pages.Admin
{
    public class EditUser : BasePage
    {
        public EditUser(ITestConfiguration baseObject) : base(baseObject)
        {
            RegisterSubElement("Create User Header", new { Text = "Create User", id = "contains=lblTitlebarHeader" });
            RegisterSubElement("Email Address", new { id = "contains=txtEmailAddress" });
            RegisterSubElement("First Name", new { id = "contains=txtFirstName" });
            RegisterSubElement("Last Name", new { id = "contains=txtLastName" });
            RegisterSubElement("Password", new { id = "contains=txtPassword" });

            RegisterSubElement("Org Grid", new { id = "contains=spOrgs_orgView" });
            RegisterSubElement("Org Grid Names", new { parentelement = "Org Grid", css = "tr td:first-child" });


            RegisterSubElement("Role Checkbox Label", new { css = "input[id*=spRoles_cblRoles] + label" });

            RegisterSubElement("Create Button", new { id = "contains=btnSaveOrCreate" });

            FindSubElement("Create User Header", 120);
            TestConfiguration.TestOutput.WriteLineWithScreenshot("Opened Create Users Page", GetScreenshot());
        }

        public Users CreateUser(string emailAddress, string firstName, string lastName, string password, string[][] orgPaths, string[] roles)
        {
            CompleteFields(emailAddress, firstName, lastName, password);

            foreach (string[] orgPath in orgPaths)
            {
                AddOrganization(orgPath);
            }

            foreach (string role in roles)
            {
                AddRole(role);
            }

            Click("Create Button");

            return New<Users>();
        }

        public void CompleteFields(string emailAddress, string firstName, string lastName, string password)
        {
            CompleteTextInput("Email Address", emailAddress);

            CompleteTextInput("First Name", firstName);

            CompleteTextInput("Last Name", lastName);

            CompleteTextInput("Password", password);
        }
        
        public void AddOrganization(string[] organizationPath)
        {
            var orgGrid = new SmartGrid(BaseObject, "spOrgs_orgView");

            var startingOrgs = orgGrid.GetColumnContents("Name");

            //No need to add it if it is already added
            if (startingOrgs.Contains(organizationPath.Last()))
            {
                return;
            }

            var orgPicker = new ExtTreePicker(BaseObject, "User Org Picker", "spOrgs_ucOrgPicker");

            orgPicker.SelectOrganization(organizationPath);

            //Wait for new elements to show up in grid
            bool orgAppeared = Wait.For(() =>
            {
                var newOrgs = orgGrid.GetColumnContents("Name");
                return newOrgs.Count > startingOrgs.Count && newOrgs.Contains(organizationPath.Last());
            }, 5, 1);


            if (!orgAppeared)
            {
                throw new ActionFailedException(string.Format("Failed to select org in path {0}", string.Join(" -> ", organizationPath)));
            }
        }

        public void AddRole(string role)
        {
            var availableRoles = FindSubElements("Role Checkbox Label");

            var targetRole = availableRoles.FirstOrDefault(e => e.InnerHtml().Equals(role, StringComparison.OrdinalIgnoreCase));

            if (targetRole != null)
            {
                var checkbox = FindSubElement(new Dictionary<string, string>(){ { "id", targetRole.GetAttribute("for") }});

                checkbox.SetChecked(true);
            }
        }
    }
}
