using Microsoft.AspNetCore.Identity;
using IdentityNLayer.Core.Entities;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface IUserService
    {
        public async void CreateAsync(Person entity, UserRoles role, string password) { }
    }   
}
