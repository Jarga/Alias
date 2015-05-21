using System.Threading;
using TestAutomation.Applications.MarketOnce.Controls.Ext;
using TestAutomation.Shared;
using TestAutomation.Shared.Exceptions;

namespace TestAutomation.Applications.MarketOnce.Pages.List
{
    public class EditTarget : WebPage
    {
        public EditTarget(ITestableWebPage baseObject) : base(baseObject)
        {
            RegisterSubElement("Target Container", new { Id = "target_container" });
            RegisterSubElement("Target Name", new { Id = "target_name_input" });
            RegisterSubElement("New Target Button", new { Id = "create_new_btn" });
            RegisterSubElement("Next Button", new { css = "#main_selection_container .next-btn" });
            RegisterSubElement("List Search", new { Id = "list_search" });

            RegisterSubElement("Target Expression Container", new { Id = "target_expression_container" });

            FindSubElement("Target Container", 120);
        }

        public bool NewTarget(string name, string listName)
        {
            Type("Target Name", name);

            Click("New Target Button");

            if (!SelectList(listName))
            {
                throw new ActionFailedException(string.Format("Failed to select list {0}", listName));
            }

            WaitForAttributeState("Next Button", "class", s => !s.Contains("disabled"), 30);

            Click("Next Button");

            return WaitFor("Target Expression Container");
        }

        //TODO: Deal with paging?
        public bool SelectList(string listName)
        {
            var extGrid = new ExtGridControl(BaseObject, "ListSelectionViewGrid");

            if (extGrid.ClickCellWithValue("Name", listName))
            {
                return true;
            }

            Type("List Search", listName);

            Thread.Sleep(5);

            return extGrid.ClickCellWithValue("Name", listName);
        }
    }
}
