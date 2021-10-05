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
    public class StudentsRepository : IRepository<Student>
    {
        private ApplicationContext _context;

        public StudentsRepository(ApplicationContext context)
        {
            _context = context;
        }        

        public void Update(Student item)
        {
            _context.Attach(item);
            _context.Entry(item).Property("Type").IsModified = true;
        }

        public async Task DeleteAsync(int id)
        {
            _context.Students.Remove(await _context.Students.FindAsync(id));
        }

        public async Task<IEnumerable<Student>> FindAsync(Expression<Func<Student, bool>> predicate)
        {
            return await _context.Students
                .Include(st => st.User)
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task CreateAsync(Student item)
        {
            await _context.Students.AddAsync(item);
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