using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomation.Shared.Enumerations
{
    [Flags]
    public enum Environments
    {
        DEV = 0,
        QA = 1,
        STAGE = 2,
        DR = 4,
        PRODUCTION = 8
    }
}
