using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityNLayer.DAL.Interfaces
{
    public interface IFilterRepository<T, K> : IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> Filter(K filter);
    }
}
