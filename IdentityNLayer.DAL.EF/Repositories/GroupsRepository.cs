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
    public class GroupsRepository : IRepository<Group>
    {
        private ApplicationContext _context;

        public GroupsRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Update(Group item)
        {
           _context.Entry(item).State = EntityState.Modified;
           _context.Groups.Update(item);
        }
        public async Task DeleteAsync(int id)
        {
            _context.Groups.Remove(await _context.Groups.Where(gr => gr.Id == id).SingleOrDefaultAsync());
        }

        public async Task<IEnumerable<Group>> FindAsync(Expression<Func<Group, bool>> predicate)
        {
            return await _context.Groups
              .Include(gr => gr.Enrollments)
              .Include(gr => gr.Methodist)
              .ThenInclude(tc => tc.User)
              .Include(gr => gr.Teacher)
              .ThenInclude(tc => tc.User)
              .AsNoTracking()
              .Where(predicate)
              .ToListAsync();
        }

        public async Task CreateAsync(Group item)
        {
            await _context.Groups.AddAsync(item);
        }

        public Task<Group> GetAsync(int id)
        {
            return _context.Groups
               .Include(g => g.Course)
               .ThenInclude(c => c.Lessons)
               .Include(g => g.Enrollments)
               .Include(g => g.Teacher)
               .ThenInclude(tc => tc.User)
               .AsNoTracking()
               .Where(g => g.Id == id)
               .SingleAsync();
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await _context.Groups
                .Include(g => g.Enrollments)
                .Include(g => g.Course)
                .Include(g => g.Methodist)
                .ThenInclude(tc => tc.User)
                .Include(g => g.Teacher)
                .ThenInclude(tc => tc.User)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
