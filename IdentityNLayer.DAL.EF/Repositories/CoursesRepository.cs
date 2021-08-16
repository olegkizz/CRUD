using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.EF.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class CoursesRepository : IRepository<Course>
    {
        private ApplicationContext _context;

        public CoursesRepository(ApplicationContext context)
        {
            _context = context;
        }
        public void Create(Course item)
        {
            _context.Courses.Add(item);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Course> Find(Func<Course, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Course Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Course> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Course item)
        {
            throw new NotImplementedException();
        }
    }
}
