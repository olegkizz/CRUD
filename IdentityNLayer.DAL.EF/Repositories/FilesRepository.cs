using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class FilesRepository : IRepository<File>
    {
        private ApplicationContext _context;

        public FilesRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async void CreateAsync(File item)
        {
            await _context.Files.AddAsync(item);
        }

        public async Task<EntityEntry<File>> DeleteAsync(int id)
        {
            return _context.Files.Remove(await _context.Files.FindAsync(id));
        }

        public async Task<IEnumerable<File>> FindAsync(Expression<Func<File, bool>> predicate)
        {
            return await _context.Files
                  .AsNoTracking()
                  .Where(predicate)
                  .ToListAsync();
        }

        public Task<IEnumerable<File>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<File> GetAsync(int id)
        {
            return await _context.Files
            .AsNoTracking()
            .Where(s => s.Id == id)
            .SingleAsync();
        }

        public void UpdateAsync(File item)
        {
            _context.Files.Update(item);
        }
    }
}
