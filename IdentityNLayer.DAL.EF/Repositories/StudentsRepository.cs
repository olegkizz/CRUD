using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityNLayer.DAL;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class StudentsRepository : IRepository<Student>
    {
        private ApplicationContext _context;

        public StudentsRepository(ApplicationContext context)
        {
            _context = context;
        }        
        public IEnumerable<Student> GetAll()
        {
            return _context.Students
               .Include(s => s.Group)
               .ToList();
        }

        public Student Get(int id)
        {
            return _context.Students.Find(id);
        }

        public IEnumerable<Student> Find(Func<Student, bool> predicate)
        {
            return _context.Students.Where(predicate).ToList();
        }

        public void Create(Student item)
        {
            _context.Students.Add(item);
        }

        public void Update(Student item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Student student = _context.Students.Find(id);
            if (student != null)
                _context.Students.Remove(student);
        }
    }
}