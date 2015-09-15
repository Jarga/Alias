using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.DataAccess.Repositories.Interfaces.Admin;
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
                    UserName = "sean.mcadams@oceansideten.com",
                    Password = ""
                }
            };
        }

        public User Get(string key)
        {
            return GetAll().FirstOrDefault(u => u.UserName.Equals(key, StringComparison.OrdinalIgnoreCase));
        }
    }
}
