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
    public class StudentsRepository : IRepository<Student>
    {
        private ApplicationContext _context;

        public StudentsRepository(ApplicationContext context)
        {
            _context = context;
        }        
        public IEnumerable<Student> Find(Func<Student, bool> predicate)
        {
            return _context.Students
                .Include(st => st.User)
                .AsNoTracking()
                .Where(predicate).ToList();
        }

        public void UpdateAsync(Student item)
        {
            _context.Attach(item);
            _context.Entry(item).Property("Type").IsModified = true;
         /*   _context.Students.Update(item));*/
        }

        public async Task<EntityEntry<Student>> DeleteAsync(int id)
        {
            return _context.Students.Remove(await _context.Students.FindAsync(id));
        }

        public async Task<IEnumerable<Student>> FindAsync(Func<Student, bool> predicate)
        {
            return await _context.Students
                .Include(st => st.User)
                .AsNoTracking()
                .Where(predicate)
                .AsQueryable()
                .ToListAsync();
        }

        public void CreateAsync(Student item)
        {
            _context.Students.AddAsync(item);
        }

        public async Task<Student> GetAsync(int id)
        {
            return await _context.Students
              .Include(s => s.User)
              .Where(s => s.Id == id)
              .AsNoTracking()
              .AsQueryable()
              .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students
                .Include(st => st.User)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}