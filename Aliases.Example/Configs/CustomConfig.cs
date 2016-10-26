using Aliases.Drivers.Selenium.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliases.Example.Configs
{
    public class CustomConfig : SeleniumTestConfiguration
    {
        public override IWebDriver StartFirefox()
        {
            return new FirefoxDriver(new FirefoxProfile() { AcceptUntrustedCertificates = true });
        }
    }
}
