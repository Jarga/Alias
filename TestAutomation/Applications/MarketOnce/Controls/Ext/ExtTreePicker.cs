using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using AutomationCore;
using AutomationCore.Shared;

namespace TestAutomation.Applications.MarketOnce.Controls.Ext
{
    public class ExtTreePicker : WebElement
    {

        private string BtnBaseCss { get; set; }
        private string BtnBaseName { get; set; }
        private string MenuBaseCss { get; set; }
        private string MenuBaseName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseObject"></param>
        /// <param name="pickerFriendlyName"></param>
        /// <param name="idPrefix">prefix for the root elements, so the {0} for: {0}_extMenu or {0}_btnMenu_Container</param>
        public ExtTreePicker(ITestableWebPage baseObject, string pickerFriendlyName, string idPrefix) : base(baseObject)
        {
            BtnBaseCss = string.Format("div[id$={0}_btnMenu_Container]", idPrefix);
            BtnBaseName = string.Format("Ext Tree Picker - {0}", pickerFriendlyName + " Btn");
            MenuBaseCss = string.Format("div[id$={0}_extMenu]", idPrefix);
            MenuBaseName = string.Format("Ext Tree Picker - {0}", pickerFriendlyName + " Menu");

            if (!SubElements.ContainsKey(BtnBaseName))
            {
                RegisterSubElement(BtnBaseName, new { css = BtnBaseCss });
                RegisterSubElement(MenuBaseName, new { css = MenuBaseCss });

                //Used for relative lookups
                RegisterSubElement(MenuBaseName + " Node Expand", new { css = ".x-tree-expander" });
                RegisterSubElement(MenuBaseName + " Text Node", new { @class = "contains=x-tree-node-text" });
            }
        }

        public void SelectOrganization(string [] orgPath)
        {
            var btn = FindSubElement(BtnBaseName);

            btn.Click();

            WaitForAppear(MenuBaseName, 30);

            var menuRoot = FindSubElement(MenuBaseName);

            for (int i = 0; i < orgPath.Length; i++)
            {
                var orgNodeProperties =
                    new Dictionary<string, string>(GetElementProperties(MenuBaseName + " Text Node"))
                    {
                        {"text", orgPath[i]}
                    };

                var orgNode = menuRoot.FindSubElement(orgNodeProperties);

                for (int j = 0; !orgNode.IsDisplayed() && j < 10; j++)
                {
                    Thread.Sleep(1000);
                }

                if (i == orgPath.Length - 1)
                {
                    orgNode.Parent().Click();

                    WaitForDisappear(MenuBaseName, 30);
                }
                else
                {
                    var parent = orgNode.Parent();

                    var expand = parent.FindSubElement(GetElementProperties(MenuBaseName + " Node Expand"));

                    var recordsBeforeExpand = menuRoot.FindSubElements(GetElementProperties(MenuBaseName + " Text Node"));

                    expand.Click();

                    Thread.Sleep(1000);

                    Stopwatch watch = new Stopwatch();
                    watch.Start();

                    bool newRecordsExist = false;
                    while (!newRecordsExist && watch.ElapsedMilliseconds < (20 * 1000))
                    {
                        var recordsAfterExpand = menuRoot.FindSubElements(GetElementProperties(MenuBaseName + " Text Node"));
                        newRecordsExist = recordsAfterExpand.Count > recordsBeforeExpand.Count;
                        if (!newRecordsExist)
                        {
                            Thread.Sleep(1000);
                        }
                    }
                    watch.Stop();
                }
            }
        }
    }
}
