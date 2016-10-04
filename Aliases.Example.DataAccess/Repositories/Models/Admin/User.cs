using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliases.Example.DataAccess.Repositories.Models.Admin
{
    public class User
    {
        public string Site { get; set; }
        public string Environment { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
