using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.TestRunner
{
    class Program
    {
        static int Main(string[] args)
        {
            //Debugging lines for now
            //string envMachine = DicToString(Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Machine));
            //string envUser = DicToString(Environment.GetEnvironmentVariables(EnvironmentVariableTarget.User));
            //string envProcess = DicToString(Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process));
            //File.WriteAllText(@"C:\env_output.txt", $"Machine:{envMachine}\r\nUser:{envUser}\r\nProcess:{envProcess}");

            return Xunit.ConsoleClient.Program.Main(args);
        }

        public static string DictionaryToString(IDictionary dictionary)
        {
            string result = string.Empty;

            foreach (var key in dictionary.Keys)
            {
                result += $"{key}={dictionary[key]}";
            }

            return result; ;
        }
    }
}
