using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomation.Shared;
using TestAutomation.Shared.Enumerations;

namespace TestAutomation.Initialization
{
    public static class GlobalTestSettings
    {
        public static Environments Environment { get; set; }

        public static ITestableWebPage BaseTestPageType { get; set; }
    }
}
