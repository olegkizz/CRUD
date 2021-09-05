using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.EF.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class TopicsRepository : IRepository<Topic>
    {
        private ApplicationContext _context;

        public TopicsRepository(ApplicationContext context)
        {
            _context = context;
        }
        public void Create(Topic item)
        {
            throw new NotImplementedException();
        }

        public void CreateAsync(Topic item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Topic> Find(Func<Topic, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Topic> FindAsync(Func<Topic, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Topic Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Topic> GetAll()
        {
            return _context.Topics;
        }

        public void Update(Topic item)
        {
            throw new NotImplementedException();
        }
    }
}
