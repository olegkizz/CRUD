

using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Person> _userManager;

        public UserService(RoleManager<IdentityRole> roleManager, UserManager<Person> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async void CreateAsync(Person entity, UserRoles role)
        {
            IdentityRole identityole = new IdentityRole();
            identityole.Name = role.ToString();
            await _roleManager.CreateAsync(identityole);

            IdentityResult chkUser = await _userManager.CreateAsync(entity, entity.PasswordHash);

            //Add default User to Role Admin    
            if (chkUser.Succeeded)
            {
                await _userManager.AddToRoleAsync(entity, role.ToString());
            }
        }
    }
}
