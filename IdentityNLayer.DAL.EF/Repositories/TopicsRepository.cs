using System;
using System.Collections.Generic;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            return _context.Topics
                    .Where(predicate)
                    .ToList();
        }

        public async Task<IEnumerable<Topic>> FindAsync(Func<Topic, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Topic>> GetAllAsync()
        {
            return await  _context.Topics.ToListAsync();
        }

        public async Task<Topic> GetAsync(int id)
        {
            return await _context.Topics.SingleAsync(t => t.Id == id);
        }

        public void Update(Topic item)
        {
            throw new NotImplementedException();
        }
    }
}
