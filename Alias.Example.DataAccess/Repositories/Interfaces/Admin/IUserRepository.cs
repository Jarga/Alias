using Alias.Example.DataAccess.Repositories.Keys.Admin;
using Alias.Example.DataAccess.Repositories.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alias.Example.DataAccess.Repositories.Interfaces.Admin
{
    public interface IUserRepository : IRepository<User, UserKey>
    {
    }
}
