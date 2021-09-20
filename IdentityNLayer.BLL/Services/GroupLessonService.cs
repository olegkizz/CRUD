using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Services
{
    public class GroupLessonService : IGroupLessonService
    {
        public Task<int> CreateAsync(GroupLesson entity)
        {
            throw new NotImplementedException();
        }

        public Task<EntityEntry<GroupLesson>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GroupLesson>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GroupLesson> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(GroupLesson entity)
        {
            throw new NotImplementedException();
        }
    }
}
