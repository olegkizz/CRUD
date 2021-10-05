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
    public class GroupLessonsRepository : IRepository<GroupLesson>
    {
        private ApplicationContext _context;

        public GroupLessonsRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(GroupLesson item)
        {
            await _context.GroupLessons.AddAsync(item);
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GroupLesson>> FindAsync(Expression<Func<GroupLesson, bool>> predicate)
        {
            return await _context.GroupLessons
               .Include(gl => gl.Group)
               .ThenInclude(gl => gl.Course)
               .Include(gl => gl.Lesson)
               .ThenInclude(gl => gl.File)
               .AsNoTracking()
               .Where(predicate)
               .ToListAsync();
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

        public void Update(GroupLesson item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.GroupLessons.Update(item);
        }
    }
}
