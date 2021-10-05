using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Services
{
    public class ManagerService : IManagerService
    {
        private IUnitOfWork Db { get; set; }
        private UserManager<Person> _userManager { get; set; }

        public ManagerService(IUnitOfWork db,
            UserManager<Person> userManager)
        {
            Db = db;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Manager>> GetAllAsync()
        {
            return await Db.Managers.GetAllAsync();
        }

        public async Task<Manager> GetByIdAsync(int id)
        {
            return (await Db.Managers.FindAsync(m => m.Id == id)).SingleOrDefault();

        }

        public async Task<int> CreateAsync(Manager entity)
        {
            await Db.Managers.CreateAsync(entity);
            await Db.Save();
            return entity.Id;
        }

        public async Task UpdateAsync(Manager entity)
        {
            Db.Managers.Update(entity);
            await Db.Save();
        }

        public async Task Delete(int id)
        {
            await Db.Managers.DeleteAsync(id);
            await Db.Save();
        }

        public async Task<Manager> GetByUserId(string userId)
        {
            return (await Db.Managers.FindAsync(m => m.UserId == userId)).SingleOrDefault();
        }
    }
}
