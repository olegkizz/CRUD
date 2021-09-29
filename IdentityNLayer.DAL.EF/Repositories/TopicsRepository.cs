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

        public async void CreateAsync(Topic item)
        {
            await _context.Topics.AddAsync(item);
        }

        public async Task<EntityEntry<Topic>> DeleteAsync(int id)
        {
            return _context.Topics.Remove(await _context.Topics.Where(t => t.Id == id)?.SingleOrDefaultAsync());
        }

        public IEnumerable<Topic> Find(Func<Topic, bool> predicate)
        {
            return _context.Topics
                    .Include(t => t.Course)
                    .Where(predicate)
                    .ToList();
        }

        public async Task<IEnumerable<Topic>> FindAsync(Func<Topic, bool> predicate)
        {
            return await _context.Topics
                  .Include(t => t.Course)
                  .Where(predicate)
                  .AsQueryable()
                  .ToListAsync();
        }

        public async Task<IEnumerable<Topic>> GetAllAsync()
        {
            return await  _context.Topics.ToListAsync();
        }

        public async Task<Topic> GetAsync(int id)
        {
            return await _context.Topics.SingleAsync(t => t.Id == id);
        }

        public void UpdateAsync(Topic item)
        {
            _context.Topics.Update(item);
        }
    }
}
