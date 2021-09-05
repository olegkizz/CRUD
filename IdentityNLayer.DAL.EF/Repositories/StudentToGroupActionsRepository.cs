using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.EF.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class StudentToGroupActionsRepository : IRepository<StudentToGroupAction>
    {
        private readonly ApplicationContext _context;
        public StudentToGroupActionsRepository(ApplicationContext context)
        {
            _context = context;
        }
        public IEnumerable<StudentToGroupAction> GetAll()
        {
            return _context.StudentToGroupActions
               .Include(s => s.Student)
               .Include(s => s.Group)
               .ToList();
        }

        public StudentToGroupAction Get(int id)
        {
            return _context.StudentToGroupActions.Find(id);
        }

        public IEnumerable<StudentToGroupAction> Find(Func<StudentToGroupAction, bool> predicate)
        {
            return _context.StudentToGroupActions.Where(predicate).ToList();
        }

        public void Create(StudentToGroupAction item)
        {
            _context.StudentToGroupActions.Add(item);
        }

        public void Update(StudentToGroupAction item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            StudentToGroupAction studentaction = _context.StudentToGroupActions.Find(id);
            if (studentaction != null)
                _context.StudentToGroupActions.Remove(studentaction);
        }

        public IEnumerable<StudentToGroupAction> FindAsync(Func<StudentToGroupAction, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void CreateAsync(StudentToGroupAction item)
        {
            throw new NotImplementedException();
        }
    }
}
