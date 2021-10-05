using System;
using System.Collections.Generic;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class ManagersRepository : IRepository<Manager>
    {
        private ApplicationContext _context;

        public ManagersRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Manager item)
        {
            await _context.Managers.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            _context.Managers.Remove(await _context.Managers.FindAsync(id));
        }

        public async Task<IEnumerable<Manager>> FindAsync(Expression<Func<Manager, bool>> predicate)
        {
            return await _context.Managers
                .Include(m => m.User)
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Manager>> GetAllAsync()
        {
            return await _context.Managers
                .Include(c => c.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Manager> GetAsync(int id)
        {
            return await _context.Managers
                .Include(c => c.User)
                .Where(m => m.Id == id)
                .AsNoTracking()
                .SingleAsync();
        }

        public void Update(Manager item)
        {
            _context.Attach(item);
            _context.Entry(item).Property(e => e.LinkToContact).IsModified = true;
        }
    }
}
