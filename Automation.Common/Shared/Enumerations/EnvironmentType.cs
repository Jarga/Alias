using System;

namespace Alias.Common.Shared.Enumerations
{
    [Flags]
    public enum EnvironmentType
    {
        Dev = 0,
        QA = 1,
        Stage = 2,
        DR = 4,
        Production = 8
    }
}
