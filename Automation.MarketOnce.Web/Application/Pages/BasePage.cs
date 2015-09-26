using System;
using System.Linq;
using Automation.Common;
using Automation.Common.Initialization;
using Automation.Common.Initialization.Interfaces;
using Automation.Common.Shared;
using Automation.Common.Shared.Exceptions;
using Automation.MarketOnce.Web.Application.Pages.Admin;

namespace Automation.MarketOnce.Web.Application.Pages
{
    public class BasePage : WebPage
    {
        public BasePage(ITestConfiguration baseObject) : base(baseObject)
        {
            RegisterSubElement("Logout", new { Id = "ctl00_ucHeader_lnkLogout" });
            RegisterSubElement("Menu Link", new { css = "[id^=ctl00_marketOnceSiteMenu_ssmSiteMenu__mnu][id$=_lnk]" });
            RegisterSubElement("Menu Link SubMenu Expand", new { css = "[id^=ctl00_marketOnceSiteMenu_ssmSiteMenu__mnu][id$=_openmenu]" });
            RegisterSubElement("Menu Link SubMenu Link", new { css = "[id^=ctl00_marketOnceSiteMenu_ssmSiteMenu__mnu][id$=_lnkChild]" });
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
                    var container = targetLink.Parent(2);

                    var expandMenu = container.FindSubElement(GetElementProperties("Menu Link SubMenu Expand"), 120);
                    var navigationTargets = container.FindSubElements(GetElementProperties("Menu Link SubMenu Link"), 120);

                    expandMenu.Click();

                    targetLink = navigationTargets.FirstOrDefault(e => e.InnerHtml().Contains(navigationPath[1]));

                    if (targetLink != null)
                    {
                        targetLink.Click();
                    }
                }

                return New<T>();
            }

            throw new Exception(string.Format("Invalid Navigation Path {0}", string.Join(", ", navigationPath ?? new string[0])));
        }

        public void CompleteTextInput(string field, string value)
        {
            var element = FindSubElement(field);

            Type(element, value);

            if (!element.GetAttribute("value").Equals(value))
            {
                throw new ActionFailedException(string.Format("Field {0} should have the value {1} but it does not!", field, value));
            }
        }

        public Welcome SwitchUser(string email, string password)
        {
            Click("Logout");

            //Verify we hit the langing page after logout
            New<LandingPage>();

            TestConfiguration.TestOutput.WriteLineWithScreenshot("Logged Out", GetScreenshot());

            return New<MarketOnceSession>().Open().LogIn(email, password);
        }
    }
}
