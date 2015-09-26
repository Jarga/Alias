using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Automation.Common;
using Automation.Common.Initialization.Interfaces;
using Automation.Common.Shared.Exceptions;
using Automation.CVS.Manage.Web.Application.Controls;
using Automation.CVS.Manage.Web.Application.Pages.Home;
using Automation.CVS.Manage.Web.Application.Pages.Projects;

namespace Automation.CVS.Manage.Web.Application
{
    public class ManageBasePage : WebPage
    {
        public ManageBasePage(ITestConfiguration testConfig) : base(testConfig)
        {
            RegisterSubElement("Page Title", new { Id = "ctl00_pageTitle" });
            RegisterSubElement("Page Description", new { Id = "ctl00_pageDescription" });

            RegisterSubElement("Change Org", new {Id = "ctl00_SiteHeader_UcLogin1_UserName_imgbtnImpersonateLogo" });
            RegisterSubElement("Logout", new { Id = "ctl00_SiteHeader_UcLogin1_CommandBar_lnkbtnLogout" });

            RegisterSubElement("Site Menu", new { Id = "ctl00_ucSiteMenu_divTabs" });

            RegisterSubElement("Home Tab", new { ParentElement = "Site Menu", Text = "Home" });
            RegisterSubElement("Projects Tab", new { ParentElement = "Site Menu", Text = "Projects" });

            RegisterSubElement("Menu Link", new { css = "#ctl00_ucSiteMenu_ulMenus a" });

            //Relative Items
            RegisterSubElement("Sub Menu", new { css = ".subnav" });
            RegisterSubElement("Sub Menu Link", new { css = ".subnav a" });
            RegisterSubElement("Sub Menu Expand Element", new { css = "span" });
        }

        public string GetPageDescription()
        {
            return FindSubElement("Page Description").InnerHtml();
        }

        public Start NavigateToHomeTab()
        {
            Click("Home Tab");
            return New<Start>();
        }
        public ProjectsDashboard NavigateToProjectsTab()
        {
            Click("Projects Tab");
            return New<ProjectsDashboard>();
        }

        /// <summary>
        /// Navigates to the marketonce page requesetd
        /// </summary>
        /// <typeparam name="T">The page that you are expecting to navigate to</typeparam>
        /// <param name="navigationPath"></param>
        /// <returns></returns>
        public T NavigateMenu<T>(params string[] navigationPath) where T : WebPage
        {
            if (navigationPath != null && navigationPath.Length >= 1)
            {
                var mainNavigationElements = FindSubElements("Menu Link", 120);
                var targetLink = mainNavigationElements.FirstOrDefault(e => e.InnerHtml().Contains(navigationPath[0]));

                if (targetLink != null && navigationPath.Length == 1)
                {
                    targetLink.Click();
                }

                if (targetLink != null && navigationPath.Length == 2)
                {
                    var container = targetLink.Parent();

                    var expandMenu = container.FindSubElement(GetElementProperties("Sub Menu Expand Element"), 120);
                    var navigationTargets = container.FindSubElements(GetElementProperties("Sub Menu Link"), 120);

                    expandMenu.Click();

                    //Wait for animation to start then wait a second for it to load
                    WaitForAppear("Sub Menu", 10);
                    Thread.Sleep(1000);

                    targetLink = navigationTargets.FirstOrDefault(e => e.InnerHtml().Contains(navigationPath[1]));

                    targetLink?.Click();
                }

                return New<T>();
            }

            throw new Exception(string.Format("Invalid Navigation Path {0}", string.Join(", ", navigationPath ?? new string[0])));
        }

        public Start ChangeOrganization(params string[] orgPath)
        {
            Click("Change Org");

            SelectOrganization selectOrg = new SelectOrganization(BaseObject, "Main Org Picker");

            selectOrg.ChangeOrg(orgPath[0]);

            Start start = New<Start>();

            if (!start.GetPageDescription().Contains(orgPath[0]))
            {
                throw new ActionFailedException("Failed to load start page for new organization.");
            }

            return start;
        }
    }
}
