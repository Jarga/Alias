using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.DataAccess.Repositories.Interfaces.Admin;
using Automation.DataAccess.Repositories.Keys.Admin;
using Automation.DataAccess.Repositories.Models.Admin;

namespace Automation.DataAccess.Repositories.Admin
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<User> GetAll()
        {
            return new List<User>()
            {
                new User()
                {
                    UserName = "",
                    Password = "",
                    Site = "MarketOnce",
                    Environment = "Dev"
                },

                new User()
                {
                    UserName = "",
                    Password = "",
                    Site = "ClearVoiceManage",
                    Environment = "Dev"
                }
            };
        }
        
        public User Get(UserKey key)
        {
            return GetAll().FirstOrDefault(u => (key.UserName == null || u.UserName.Equals(key.UserName, StringComparison.OrdinalIgnoreCase)) 
                                                && (key.Site == null || u.Site.Equals(key.Site, StringComparison.OrdinalIgnoreCase))
                                                && (key.Environment == null || u.Environment.Equals(key.Environment, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
