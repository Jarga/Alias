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
        private static DependencyResolver _container = new DependencyResolver();

        static RepositoryProvider()
        {
            _container.Register<IUserRepository>(resolver => new UserRepository());
        }

        public static T Get<T>()
        {
            return _container.Create<T>();
        }
    }
}
