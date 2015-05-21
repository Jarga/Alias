using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestAutomation.Shared;

namespace TestAutomation.Applications.MarketOnce.Controls.Ext
{
    public class ExtTreePicker
    {
        private ITestableWebPage _baseObject;

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
        public ExtTreePicker(ITestableWebPage baseObject, string pickerFriendlyName, string idPrefix)
        {
            _baseObject = baseObject;

            BtnBaseCss = string.Format("div[id$={0}_btnMenu_Container]", idPrefix);
            BtnBaseName = string.Format("Ext Tree Picker - {0}", pickerFriendlyName + " Btn");
            MenuBaseCss = string.Format("div[id$={0}_extMenu]", idPrefix);
            MenuBaseName = string.Format("Ext Tree Picker - {0}", pickerFriendlyName + " Menu");

            if (!_baseObject.SubElements.ContainsKey(BtnBaseName))
            {
                _baseObject.RegisterSubElement(BtnBaseName, new { css = BtnBaseCss });
                _baseObject.RegisterSubElement(MenuBaseName, new { css = MenuBaseCss });

                //Used for relative lookups
                _baseObject.RegisterSubElement(MenuBaseName + " Node Expand", new { css = ".x-tree-expander" });
                _baseObject.RegisterSubElement(MenuBaseName + " Text Node", new { @class = "x-tree-node-text" });
            }
        }

        public void SelectOrganization(string [] orgPath)
        {
            var btn = _baseObject.FindSubElement(BtnBaseName);

            btn.Click();

            _baseObject.WaitForAppear(MenuBaseName, 30);

            var menuRoot = _baseObject.FindSubElement(MenuBaseName);

            for (int i = 0; i < orgPath.Length; i++)
            {
                var orgNodeProperties = _baseObject.GetElementProperties(MenuBaseName + " Text Node");
                orgNodeProperties.Add("text", orgPath[i]);

                var orgNode = menuRoot.FindSubElement(orgNodeProperties);

                if (i == orgPath.Length - 1)
                {
                    orgNode.Click();

                    _baseObject.WaitForDisappear(MenuBaseName, 30);
                }
                else
                {
                    var parent = orgNode.Parent();

                    var expand = parent.FindSubElement(_baseObject.GetElementProperties(MenuBaseName + " Node Expand"));

                    var recordsBeforeExpand = menuRoot.FindSubElements(_baseObject.GetElementProperties(MenuBaseName + " Text Node"));

                    expand.Click();

                    Thread.Sleep(2);

                    Stopwatch watch = new Stopwatch();
                    watch.Start();

                    bool newRecordsExist = false;
                    while (!newRecordsExist && watch.ElapsedMilliseconds < (20 * 1000))
                    {
                        var recordsAfterExpand = menuRoot.FindSubElements(_baseObject.GetElementProperties(MenuBaseName + " Text Node"));
                        newRecordsExist = recordsAfterExpand.Count > recordsBeforeExpand.Count;
                        if (!newRecordsExist)
                        {
                            Thread.Sleep(2);
                        }
                    }
                    watch.Stop();
                }
            }
        }
    }
}
