using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alias.Common.Shared
{
    public interface IDialog
    {
        /// <summary>
        /// Gets the text of the alert.
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Dismisses the alert.
        /// </summary>
        void Dismiss();

        /// <summary>
        /// Accepts the alert.
        /// </summary>
        void Accept();

        /// <summary>
        /// Sends keys to the alert.
        /// </summary>
        /// <param name="keysToSend">The keystrokes to send.</param>
        void SendKeys(string keysToSend);

        /// <summary>
        /// Sets the user name and password in an alert prompting for credentials.
        /// </summary>
        /// <param name="userName">The user name to set.</param>
        /// <param name="password">The password to set.</param>
        void SetAuthenticationCredentials(string userName, string password);
    }
}
