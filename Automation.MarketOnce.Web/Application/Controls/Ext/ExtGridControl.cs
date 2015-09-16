using System.Linq;
using Automation.Common;
using Automation.Common.Shared;
using Automation.Common.Shared.Exceptions;

namespace Automation.MarketOnce.Web.Application.Controls.Ext
{
    public class ExtGridControl : WebElement
    {
        private string BaseCss { get; set; }
        private string BaseName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseObject"> The current running web page</param>
        /// <param name="gridId">Grid ID if available</param>
        public ExtGridControl(ITestableWebPage baseObject, string gridId = null) : base(baseObject)
        {
            if (!SubElements.ContainsKey("Mask"))
            {
                RegisterSubElement("Mask", new { css = ".x-mask" });
            }

            BaseCss = string.Format("div{0}.x-grid", "#" + gridId);
            BaseName = string.Format("Ext Grid - {0}", gridId);

            if (!SubElements.ContainsKey(BaseName))
            {
                RegisterSubElement(BaseName, new { css = BaseCss });
                RegisterSubElement(BaseName + " Table", new { css = BaseCss + " table.x-grid-table" });
                RegisterSubElement(BaseName + " Table Header", new { css = BaseCss + " div.x-grid-header" });
                RegisterSubElement(BaseName + " Table Header Text", new { css = BaseCss + " span.x-column-header-text" });
                RegisterSubElement(BaseName + " Table Rows", new { css = BaseCss + " tr.x-grid-row" });
                RegisterSubElement(BaseName + " Table Cell", new { css = BaseCss + " td.x-grid-cell" });
                RegisterSubElement(BaseName + " Table Cell Content", new { css = BaseCss + " div.x-grid-cell-inner" });
            }

        }
        
        public string GetColumnId(string columnName)
        {
            var columns = FindSubElements(BaseName + " Table Header Text");

            var column = columns.FirstOrDefault(c => c.InnerHtml().ToUpperInvariant().Equals(columnName.ToUpperInvariant()));

            if (column == null)
            {
                throw new ObjectNotFoundException(string.Format("Unable to find column with name: {0}", columnName));
            }

            return column.Parent(2).GetAttribute("id");
        }

        public ITestableWebElement FindCellWithValue(string columnName, string value)
        {

            //Wait for the mask to appear in case it hasn't yet then wait for it to go away
            WaitForAppear("Mask", 3);
            WaitForDisappear("Mask", 60);

            string columnCellContents = string.Format(BaseName + " Column-{0}-Contents", columnName);

            if (!SubElements.ContainsKey(columnCellContents))
            {
                string columnId = GetColumnId(columnName);

                if (string.IsNullOrWhiteSpace(columnId))
                {
                    return null;
                }

                RegisterSubElement(columnCellContents, new { css = BaseCss + string.Format(" td.x-grid-cell-{0} div.x-grid-cell-inner", columnId)});
            }

            var cellContents = FindSubElements(columnCellContents);

            return cellContents.FirstOrDefault(c => c.InnerHtml().ToUpperInvariant().Equals(value.ToUpperInvariant()));
        }

        public bool ClickCellWithValue(string columnName, string value)
        {
            var cell = FindCellWithValue(columnName, value);

            if (cell == null)
            {
                return false;
            }

            cell.Click();
            
            return true;
        }
    }
}
