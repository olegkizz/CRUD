using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void CreateAsync(File item)
        {
            _context.Files.AddAsync(item);
        }

        public async Task<EntityEntry<File>> DeleteAsync(int id)
        {
            return _context.Files.Remove(await _context.Files.FindAsync(id));
        }

        public IEnumerable<File> Find(Func<File, bool> predicate)
        {
            return _context.Files
                .AsNoTracking()
                .Where(predicate)
                .ToList();
        }

        public Task<IEnumerable<File>> FindAsync(Func<File, bool> predicate)
        {
            throw new NotImplementedException();
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
