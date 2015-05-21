using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomation.Applications.MarketOnce.Controls.Ext;
using TestAutomation.Shared;

namespace TestAutomation.Applications.MarketOnce.Pages.Admin
{
    public class EditUser : MarketOnceBasePage
    {
        public EditUser(ITestableWebPage baseObject) : base(baseObject)
        {
            RegisterSubElement("Create User Header", new { Text = "Create User", id = "contains=lblTitlebarHeader" });
            RegisterSubElement("Email Address", new { id = "contains=txtEmailAddress" });
            RegisterSubElement("First Name", new { id = "contains=txtFirstName" });
            RegisterSubElement("Last Name", new { id = "contains=txtLastName" });
            RegisterSubElement("Password", new { id = "contains=txtPassword" });


            RegisterSubElement("Role Checkbox Label", new { css = "input[id*=spRoles_cblRoles]" });

            FindSubElement("Create User Header", 120);
        }

        public bool CreateUser(string emailAddress, string firstName, string lastName, string password, string[][] orgPaths, string[] roles)
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

            return true;
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
            var orgPicker = new ExtTreePicker(BaseObject, "User Org Picker", "spOrgs_ucOrgPicker");

            orgPicker.SelectOrganization(organizationPath);
        }

        public void AddRole(string role)
        {
            var availableRoles = FindSubElements("Role Checkbox Label");

            var targetRole = availableRoles.FirstOrDefault(e => e.InnerHtml().Equals(role, StringComparison.OrdinalIgnoreCase));

            if (targetRole != null)
            {
                var checkbox = FindSubElement(new Dictionary<string, string>(){ { "id", targetRole.GetAttribute("for") }});

                checkbox.Click();
            }
        }
    }
}
