using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.DAL.Entities;

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
            throw new NotImplementedException();
        }

        public IEnumerable<Teacher> Find(Func<Teacher, bool> predicate)
        {
            throw new NotImplementedException();
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
    }
}
