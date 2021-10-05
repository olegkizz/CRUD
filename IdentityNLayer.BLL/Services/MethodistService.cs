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
    public class MethodistService : IMethodistService
    {
        private IUnitOfWork Db { get; set; }
        private UserManager<Person> _userManager { get; set; }

        public MethodistService(IUnitOfWork db,
            UserManager<Person> userManager)
        {
            Db = db;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Methodist>> GetAllAsync()
        {
            return await Db.Methodists.GetAllAsync();
        }

        public async Task<Methodist> GetByIdAsync(int id)
        {
            return (await Db.Methodists.FindAsync(m => m.Id == id)).SingleOrDefault();

        }

        public async Task<int> CreateAsync(Methodist entity)
        {
            await Db.Methodists.CreateAsync(entity);
            await Db.Save();
            return entity.Id;
        }

        public async Task UpdateAsync(Methodist entity)
        {
            Db.Methodists.Update(entity);
            await Db.Save();
        }

        public async Task Delete(int id)
        {
            await Db.Methodists.DeleteAsync(id);
            await Db.Save();
        }

        public async Task<Methodist> GetByUserId(string userId)
        {
            return (await Db.Methodists.FindAsync(m => m.UserId == userId)).SingleOrDefault();
        }
    }
}
