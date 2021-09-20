using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class GroupLessonsRepository : IRepository<GroupLesson>
    {
        public void CreateAsync(GroupLesson item)
        {
            throw new NotImplementedException();
        }

        public Task<EntityEntry<GroupLesson>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GroupLesson> Find(Func<GroupLesson, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GroupLesson>> FindAsync(Func<GroupLesson, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GroupLesson>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GroupLesson> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(GroupLesson item)
        {
            throw new NotImplementedException();
        }
    }
}
