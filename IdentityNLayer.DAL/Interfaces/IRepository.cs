using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNLayer.DAL
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        Task<IEnumerable<T>> FindAsync(Func<T, Boolean> predicate);
        void CreateAsync(T item);
        void UpdateAsync(T item);
        Task<EntityEntry<T>> DeleteAsync(int id);
    }   
}
