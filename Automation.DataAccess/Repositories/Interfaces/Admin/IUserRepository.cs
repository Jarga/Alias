using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.DataAccess.Repositories.Keys.Admin;
using Automation.DataAccess.Repositories.Models.Admin;

namespace Automation.DataAccess.Repositories.Interfaces.Admin
{
    public interface IUserRepository : IRepository<User, UserKey>
    {
    }
}
