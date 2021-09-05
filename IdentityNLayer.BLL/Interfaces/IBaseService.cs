using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        int Create(T entity);
        int CreateAsync(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
