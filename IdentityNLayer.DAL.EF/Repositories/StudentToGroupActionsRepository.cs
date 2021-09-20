using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class StudentToGroupActionsRepository : IRepository<StudentToGroupAction>
    {
        private readonly ApplicationContext _context;
        public StudentToGroupActionsRepository(ApplicationContext context)
        {
            _context = context;
        }
        public IEnumerable<StudentToGroupAction> Find(Func<StudentToGroupAction, bool> predicate)
        {
            return _context.StudentToGroupActions.Where(predicate).ToList();
        }
        public void Create(StudentToGroupAction item)
        {
            _context.StudentToGroupActions.Add(item);
        }

        public void UpdateAsync(StudentToGroupAction item)
        {
            _context.StudentToGroupActions.Update(item);
        }

        public async Task<EntityEntry<StudentToGroupAction>> DeleteAsync(int id)
        {
             return  _context.StudentToGroupActions.Remove(await _context.StudentToGroupActions.FindAsync(id));
        }

        public Task<IEnumerable<StudentToGroupAction>> FindAsync(Func<StudentToGroupAction, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void CreateAsync(StudentToGroupAction item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentToGroupAction>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StudentToGroupAction> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
