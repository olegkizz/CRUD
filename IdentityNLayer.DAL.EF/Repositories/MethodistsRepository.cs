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
    public class MethodistsRepository : IRepository<Methodist>
    {
        private ApplicationContext _context;

        public MethodistsRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Methodist item)
        {
            await _context.Methodists.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            _context.Methodists.Remove(await _context.Methodists.FindAsync(id));
        }

        public async Task<IEnumerable<Methodist>> FindAsync(Expression<Func<Methodist, bool>> predicate)
        {
            return await _context.Methodists
                .Include(m => m.User)
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Methodist>> GetAllAsync()
        {
            return await _context.Methodists
                .Include(c => c.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Methodist> GetAsync(int id)
        {
            return await _context.Methodists
                .Include(c => c.User)
                .Where(m => m.Id == id)
                .AsNoTracking()
                .SingleAsync();
        }

        public void Update(Methodist item)
        {
            _context.Attach(item);
            _context.Entry(item).Property(e => e.LinkToContact).IsModified = true;
        }
    }
}
