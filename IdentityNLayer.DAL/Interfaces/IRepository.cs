using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNLayer.DAL
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T Get(int id);
        Task<T> GetAsync(int id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        IEnumerable<T> FindAsync(Func<T, Boolean> predicate);
        void Create(T item);
        void CreateAsync(T item);
        void Update(T item);
        void Delete(int id);
    }   
}
