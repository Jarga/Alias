using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomation.Shared;

namespace TestAutomation.Applications.MarketOnce.Pages.List
{
    public class EditTarget : WebPage
    {
        public EditTarget(ITestableWebPage baseObject) : base(baseObject)
        {
            RegisterSubElement("Target Container", new { Id = "target_container" });
            RegisterSubElement("New Target Button", new { Id = "create_new_btn" });
            RegisterSubElement("List Table", new { css = "div#list_selection_container table" });
            RegisterSubElement("List Table Rows", new { css = "div#list_selection_container table .x-grid-row" });

            RegisterSubElement("List Table Cell Content", new { css = ".x-grid-cell-inner" });

            FindSubElement("Target Container", 120);
        }

        public void CreateNewTarget(string name, string listName)
        {
            
        }

        public void SelectList(string name)
        {
            //Get all table cells
            //Search text content
            //Click row
            //Verify selected
        }
    }
}
