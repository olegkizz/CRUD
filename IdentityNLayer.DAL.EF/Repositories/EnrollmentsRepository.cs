using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class EnrollmentsRepository : IRepository<Enrollment>
    {
        private ApplicationContext _context;

        public EnrollmentsRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void UpdateAsync(Enrollment item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.Enrollments.Update(item);
        }

        public async Task<EntityEntry<Enrollment>> DeleteAsync(int id)
        {
             return _context.Enrollments.Remove(await _context.Enrollments.FindAsync(id));
        }

        public async Task<IEnumerable<Enrollment>> FindAsync(Expression<Func<Enrollment, bool>> predicate)
        {
            return await _context.Enrollments
                .Include(en => en.User)
                .Where(predicate)
                .ToListAsync();
        }

        public async void CreateAsync(Enrollment item)
        {
            await _context.Enrollments.AddAsync(item);
        }

        public async Task<Enrollment> GetAsync(int id)
        {
            return await _context.Enrollments
             .Include(s => s.User)
             .AsNoTracking()
             .Where(s => s.Id == id)
             .SingleAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            return await _context.Enrollments
               .Include(s => s.User)
               .AsNoTracking()
               .ToListAsync();
        }
    }
}
