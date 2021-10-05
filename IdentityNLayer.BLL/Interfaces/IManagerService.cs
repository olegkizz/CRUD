using IdentityNLayer.Core.Entities;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface IManagerService : IBaseService<Manager>
    {
        Task<Manager> GetByUserId(string userId);
    }
}
