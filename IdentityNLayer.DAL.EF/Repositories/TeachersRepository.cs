using System;
using System.Collections.Generic;
using System.Linq;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.Core.Entities;
using System.Data.Entity;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class TeachersRepository : IRepository<Teacher>
    {
        private ApplicationContext _context;

        public TeachersRepository(ApplicationContext context)
        {
            _context = context;
        }
        public IEnumerable<Teacher> GetAll()
        {
            return _context.Teachers.ToList();
        }

        public Teacher Get(int id)
        {
            return _context.Teachers
                .Include(s => s.User)
                .Where(s => s.Id == id)
                .First();
        }

        public IEnumerable<Teacher> Find(Func<Teacher, bool> predicate)
        {
            return _context.Teachers.Where(predicate).ToList();
        }

        public void Create(Teacher item)
        {
            _context.Teachers.Add(item);
        }

        public void Update(Teacher item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Teacher> FindAsync(Func<Teacher, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void CreateAsync(Teacher item)
        {
            throw new NotImplementedException();
        }
    }
}
