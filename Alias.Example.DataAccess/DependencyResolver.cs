using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alias.Example.DataAccess
{
    public class DependencyResolver
    {
        private readonly Dictionary<string, object> _configuration = new Dictionary<string, object>();
        private readonly Dictionary<Type, Func<DependencyResolver, object>> _typeToCreator = new Dictionary<Type, Func<DependencyResolver, object>>();

        public Dictionary<string, object> Configuration => _configuration;

        public void Register<T>(Func<DependencyResolver, object> creator)
        {
            _typeToCreator.Add(typeof(T), creator);
        }

        public T Create<T>()
        {
            return (T)_typeToCreator[typeof(T)](this);
        }

        public T GetConfiguration<T>(string name)
        {
            return (T)_configuration[name];
        }
    }
}
