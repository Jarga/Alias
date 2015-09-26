using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Common;
using Automation.Common.Shared;
using Automation.Common.Shared.Exceptions;

namespace Automation.CVS.Manage.Web.Application.Controls
{
    public class SelectOrganization : WebElement
    {
        public string BaseName { get; set; }
        public SelectOrganization(ITestableWebElement baseObject, string pickerFriendlyName) : base(baseObject)
        {
            BaseName = pickerFriendlyName + " Root";

            if (!SubElements.ContainsKey(BaseName))
            {
                RegisterSubElement(BaseName, new { Id = "ctl00_SiteHeader_UcLogin1_UserName_tvOrgs" });
                RegisterSubElement(BaseName + " Org Picker Node", new { ParentElement = BaseName, css = ".org_picker_node" });
            }
        }

        /// <summary>
        /// TODO: This needs to handle multiple levels
        /// </summary>
        /// <param name="orgName"></param>
        public void ChangeOrg(string orgName)
        {
            var visibleOrgs = FindSubElements(BaseName + " Org Picker Node");

            var targetOrg = visibleOrgs.FirstOrDefault(e => e.InnerHtml().Contains(orgName));

            if (targetOrg != null)
            {
                targetOrg.Click();
            }
            else
            {
                throw new ObjectNotFoundException($"Unable to find org link with test {orgName}");
            }
        }
    }
}
