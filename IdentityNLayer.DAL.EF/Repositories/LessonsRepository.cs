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
    public class LessonsRepository : IRepository<Lesson>
    {
        private ApplicationContext _context;

        public LessonsRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Lesson item)
        {
            await _context.Lessons.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            _context.Lessons.Remove(await _context.Lessons.FindAsync(id));
        }

        public IEnumerable<Lesson> Find(Func<Lesson, bool> predicate)
        {
            return _context.Lessons
              .AsNoTracking()
              .Where(predicate)
              .ToList();
        }

        public async Task<IEnumerable<Lesson>> FindAsync(Expression<Func<Lesson, bool>> predicate)
        {
            return await _context.Lessons
              .Include(l => l.Course)
              .AsNoTracking()
              .Where(predicate)
              .ToListAsync();
        }

        public Task<IEnumerable<Lesson>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Lesson> GetAsync(int id)
        {
            return _context.Lessons
                .Include(l => l.File)
                .Include(l => l.Course)
                .AsNoTracking()
                .Where(l => l.Id == id)
                .SingleOrDefaultAsync();
        }

        public void Update(Lesson item)
        {
            _context.Attach(item);
            _context.Entry(item).Property("Name").IsModified = true;
            _context.Entry(item).Property("Theme").IsModified = true;
            _context.Entry(item).Property("Duration").IsModified = true;
            _context.Entry(item).Reference("File").IsModified = true;
            _context.Entry(item).Property("FileId").IsModified = true;
        }
    }
}
