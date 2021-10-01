using System;
using System.Collections.Generic;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class TeachersRepository : IRepository<Teacher>
    {
        private ApplicationContext _context;

        public TeachersRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void UpdateAsync(Teacher item)
        {

            _context.Attach(item);
            _context.Entry(item).Property(e => e.Bio).IsModified = true;
            _context.Entry(item).Property(e => e.LinkToProfile).IsModified = true;
        }

        public Task<EntityEntry<Teacher>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Teacher>> FindAsync(Expression<Func<Teacher, bool>> predicate)
        {
            return await _context.Teachers
               .Include(tc => tc.User)
               .Where(predicate)
               .ToListAsync();
        }

        public async void CreateAsync(Teacher item)
        {
            await _context.Teachers.AddAsync(item);
        }

        public async Task<Teacher> GetAsync(int id)
        {
            return await _context.Teachers
               .Include(s => s.User)
               .Where(s => s.Id == id)
               .AsNoTracking()
               .SingleAsync();
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await _context.Teachers
                .Include(tc => tc.User)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
