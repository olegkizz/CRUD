using System;
using System.Collections.Generic;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;



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
                .AsNoTracking()
                .Where(predicate).ToList();
        }

        public void Update(Student item)
        {
            _context.Students.Attach(item);
            
            _context.Entry(item).Property(e => e.FirstName).IsModified = true;
            _context.Entry(item).Property(e => e.LastName).IsModified = true;
            _context.Entry(item).Property(e => e.BirthDate).IsModified = true;
            _context.Entry(item).Property(e => e.Type).IsModified = true;
        }

        public void Delete(int id)
        {
            Student student = _context.Students.Find(id);
            if (student != null)
                _context.Students.Remove(student);
        }

        public async Task<IEnumerable<Student>> FindAsync(Func<Student, bool> predicate)
        {
            return await _context.Students
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
                .AsNoTracking()
                .ToListAsync();
        }
    }
}