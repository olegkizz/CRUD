using System;
using System.Collections.Generic;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class GroupsRepository : IRepository<Group>
    {
        private ApplicationContext _context;

        public GroupsRepository(ApplicationContext context)
        {
            _context = context;
        }
        public IEnumerable<Group> Find(Func<Group, bool> predicate)
        {
            return _context.Groups
                  .Include(gr => gr.Enrollments)
                  .Include(gr => gr.Teacher)
                  .ThenInclude(tc => tc.User)
                  .AsNoTracking()
                  .Where(predicate)
                  .ToList();
        }

        public void UpdateAsync(Group item)
        {
           _context.Entry(item).State = EntityState.Modified;
           _context.Groups.Update(item);
        }
        public Task<EntityEntry<Group>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Group>> FindAsync(Func<Group, bool> predicate)
        {
            return await _context.Groups
              .Include(gr => gr.Enrollments)
              .Include(gr => gr.Teacher)
              .ThenInclude(tc => tc.User)
              .AsNoTracking()
              .Where(predicate)
              .AsQueryable()
              .ToListAsync();
        }

        public async void CreateAsync(Group item)
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
                .Include(g => g.Teacher)
                .ThenInclude(tc => tc.User)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
