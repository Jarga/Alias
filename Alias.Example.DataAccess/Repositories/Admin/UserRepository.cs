using Alias.Example.DataAccess.Repositories.Interfaces.Admin;
using Alias.Example.DataAccess.Repositories.Keys.Admin;
using Alias.Example.DataAccess.Repositories.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alias.Example.DataAccess.Repositories.Admin
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<User> GetAll()
        {
            return new List<User>()
            {
                //DEV Log Ins
                new User()
                {
                    UserName = "example@example.com",
                    Password = "Example1234",
                    Site = "ExampleSite",
                    Environment = "Dev"
                },

                new User()
                {
                    UserName = "example@example.com",
                    Password = "Example1234QA",
                    Site = "ExampleSiteQA",
                    Environment = "QA"
                },

                new User()
                {
                    UserName = "example@example.com",
                    Password = "Example1234Etc",
                    Site = "ExampleSiteEtc",
                    Environment = "Etc"
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
