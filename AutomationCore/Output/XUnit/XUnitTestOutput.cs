using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace AutomationCore.Output.XUnit
{
    public class XUnitTestOutput : ITestOutput
    {
        protected ITestOutputHelper Output;

        public XUnitTestOutput(ITestOutputHelper output)
        {
            Output = output;
        }

        public void WriteLine(string message)
        {
            Output.WriteLine(message);
        }

        public void WriteLine(string format, params object[] args)
        {
            Output.WriteLine(format, args);
        }

        public void WriteLineWithScreenshot(string message, string base64Image)
        {
            Output.WriteLine(message + $"<img src=data:image/png;base64,{base64Image}>");
        }

        public void WriteLineWithScreenshot(string format, string base64Image, params object[] args)
        {
            Output.WriteLine(string.Format(format, args) + $"<img src=data:image/png;base64,{base64Image}>");
        }
    }
}
