using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityNLayer.DAL;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.DAL.Entities;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class StudentsRepository : IRepository<Student>
    {
        private StudentContext _context;

        public StudentsRepository(StudentContext context)
        {
            _context = context;
        }        
        public IEnumerable<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public Student Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> Find(Func<Student, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Create(Student item)
        {
            _context.Students.Add(item);
        }

        public void Update(Student item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}