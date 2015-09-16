using System;

namespace Automation.Common.Shared.Enumerations
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
