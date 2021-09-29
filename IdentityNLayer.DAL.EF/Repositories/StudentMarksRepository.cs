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
    public class StudentMarksRepository : IRepository<StudentMark>
    {
        private ApplicationContext _context;

        public StudentMarksRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async void CreateAsync(StudentMark item)
        {
            await _context.StudentMarks.AddAsync(item);
        }


        public IEnumerable<StudentMark> Find(Func<StudentMark, bool> predicate)
        {
            return _context.StudentMarks
                 .Include(en => en.Lesson)
                 .Include(en => en.Student)
                 .Where(predicate)
                 .AsQueryable()
                 .ToList();
        }

        public async Task<IEnumerable<StudentMark>> FindAsync(Func<StudentMark, bool> predicate)
        {
            return await _context.StudentMarks
              .Include(en => en.Lesson)
              .Include(en => en.Student)
              .Where(predicate)
              .AsQueryable()
              .ToListAsync();
        }

        public async Task<IEnumerable<StudentMark>> GetAllAsync()
        {
            return await _context.StudentMarks
                .Include(c => c.Lesson)
                .Include(en => en.Student)
                .ToListAsync();
        }

        public async Task<StudentMark> GetAsync(int id)
        {
            return await _context.StudentMarks
                .Include(c => c.Lesson)
                .Include(en => en.Student)
                .AsNoTracking()
                .Where(crs => crs.Id == id)
                .SingleAsync();
        }

        public void UpdateAsync(StudentMark item)
        {
            _context.Attach(item);
            _context.Entry(item).Property("Mark").IsModified = true;
        }

        async Task<EntityEntry<StudentMark>> IRepository<StudentMark>.DeleteAsync(int id)
        {
            return _context.StudentMarks.Remove(
                    await _context.StudentMarks.Where(sm => sm.Id == id).SingleOrDefaultAsync());
        }
    }
}
