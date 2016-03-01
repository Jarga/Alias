using Aliases.Common.Shared;
using OpenQA.Selenium;

namespace Aliases.Drivers.Selenium
{
    public class SeleniumDialog : IDialog
    {
        public IWebDriver Driver;

        public SeleniumDialog(IWebDriver driver)
        {
            Driver = driver;
        }

        public string Text => Driver.SwitchTo().Alert().Text;

        public void Accept()
        {
            Driver.SwitchTo().Alert().Accept();
        }

        public void Dismiss()
        {
            Driver.SwitchTo().Alert().Dismiss();
        }

        public void SendKeys(string keysToSend)
        {
            Driver.SwitchTo().Alert().SendKeys(keysToSend);
        }

        public void SetAuthenticationCredentials(string userName, string password)
        {
            Driver.SwitchTo().Alert().SetAuthenticationCredentials(userName, password);
        }
    }
}
