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
    public class CoursesRepository : IRepository<Course>
    {
        private ApplicationContext _context;

        public CoursesRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async void CreateAsync(Course item)
        {
            await _context.Courses.AddAsync(item);
        }

        public Task<EntityEntry<Course>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Course>> FindAsync(Expression<Func<Course, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses
                .Include(c => c.Topic)
                .Include(c => c.Lessons)
                .ThenInclude(l => l.File)
                .ToListAsync();
        }

        public async Task<Course> GetAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.Lessons)
                .ThenInclude(l => l.File)
                .AsNoTracking()
                .Where(crs => crs.Id == id)
                .SingleAsync();
        }

        public void UpdateAsync(Course item)
        {
            _context.Courses.Update(item);
        }
    }
}
