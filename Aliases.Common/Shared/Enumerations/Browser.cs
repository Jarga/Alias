using System;

namespace Aliases.Common.Shared.Enumerations
{
    [Flags]
    public enum Browser
    {
        Chrome = 1,
        Firefox = 2,
        IE = 4,
        Safari = 8,
        PhantomJS = 16,
        Android = 32,
        IPhone = 64,
        IPad = 128,
        Edge = 256
    }
}
