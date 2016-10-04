using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliases.Example.DataAccess.Repositories.Interfaces
{
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(TKey key);
    }
}
