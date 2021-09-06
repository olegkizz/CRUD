using System;
using System.Collections.Generic;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class GroupsRepository : IRepository<Group>
    {
        private ApplicationContext _context;

        public GroupsRepository(ApplicationContext context)
        {
            _context = context;
        }
        public IEnumerable<Group> GetAll()
        {
            return _context.Groups
                .Include(g => g.Enrollments)
                .Include(g => g.Teacher);
        }

        public Group Get(int id)
        {
            return _context.Groups
                .Include(g => g.Enrollments)
                .Include(g => g.Teacher)
                .Where(g => g.Id == id)
                .First();
        }

        public IEnumerable<Group> Find(Func<Group, bool> predicate)
        {
            return _context.Groups
                .Include(gr => gr.Teacher)
                .Include(gr => gr.Enrollments)
                .AsNoTracking()
                .Where(predicate).ToList();
        }

        public void Create(Group item)
        {
            _context.Groups.Add(item);
        }

        public void Update(Group item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.Groups.Update(item);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> FindAsync(Func<Group, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void CreateAsync(Group item)
        {
            throw new NotImplementedException();
        }

        public Task<Group> GetAsync(int id)
        {
            return _context.Groups
               .Include(g => g.Enrollments)
               .Include(g => g.Teacher)
               .Where(g => g.Id == id)
               .FirstAsync();
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await _context.Groups
                .Include(g => g.Enrollments)
                .Include(g => g.Teacher)
                .ToListAsync();
        }
    }
}
