using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomation.Shared;

namespace TestAutomation.Applications.MarketOnce.Controls.Ext
{
    public class ExtGridControl
    {
        private ITestableWebElement _baseObject;

        public ExtGridControl(ITestableWebElement baseObject)
        {
            _baseObject = baseObject;

        }
    }
}
