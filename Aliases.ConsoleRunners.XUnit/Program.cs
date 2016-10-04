using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliases.ConsoleRunners.XUnit
{
    class Program
    {
        static int Main(string[] args)
        {
            return Xunit.ConsoleClient.Program.Main(args);
        }
    }
}
