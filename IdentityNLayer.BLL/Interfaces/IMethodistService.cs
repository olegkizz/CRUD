using IdentityNLayer.Core.Entities;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface IMethodistService : IBaseService<Methodist>
    {
        Task<Methodist> GetByUserId(string userId);
    }
}
