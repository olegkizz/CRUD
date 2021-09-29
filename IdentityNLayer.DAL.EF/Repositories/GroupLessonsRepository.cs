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
    public class GroupLessonsRepository : IRepository<GroupLesson>
    {
        private ApplicationContext _context;

        public GroupLessonsRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async void CreateAsync(GroupLesson item)
        {
            await _context.GroupLessons.AddAsync(item);
        }

        public Task<EntityEntry<GroupLesson>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GroupLesson> Find(Func<GroupLesson, bool> predicate)
        {
            return _context.GroupLessons
               .Include(gl => gl.Group)
               .ThenInclude(gl => gl.Course)
               .Include(gl => gl.Lesson)
               .ThenInclude(gl => gl.File)
               .AsNoTracking()
               .Where(predicate)
               .ToList();
        }

        public Task<IEnumerable<GroupLesson>> FindAsync(Func<GroupLesson, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GroupLesson>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GroupLesson> GetAsync(int id)
        {
            return _context.GroupLessons
              .Include(g => g.Group)
              .ThenInclude(gl => gl.Course)
              .Include(g => g.Lesson)
              .ThenInclude(g => g.File)
              .AsNoTracking()
              .Where(g => g.Id == id)
              .SingleAsync();
        }

        public void UpdateAsync(GroupLesson item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.GroupLessons.Update(item);
        }
    }
}
