using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.DataAccess.Repositories.Admin;
using Automation.DataAccess.Repositories.Interfaces.Admin;

namespace Automation.DataAccess
{
    public static class RepositoryProvider
    {
        private static readonly DependencyResolver Container = new DependencyResolver();

        static RepositoryProvider()
        {
            Container.Register<IUserRepository>(resolver => new UserRepository());
        }

        public static T Get<T>()
        {
            return Container.Create<T>();
        }
    }
}
