using Aliases.Drivers.Selenium.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;

namespace Aliases.Example.CustomConfigs
{
    public class CustomSeleniumConfiguration : SeleniumTestConfiguration
    {
        public override ChromeOptions GetChromeOptions()
        {
            var options = base.GetChromeOptions();

            if (options == null)
            {
                options = new ChromeOptions();
            }

            options.Proxy = new OpenQA.Selenium.Proxy()
            {
                HttpProxy = "http://localhost:8888",
                SslProxy = "http://localhost:8888"

            };

            return options;
        }
    }
}
