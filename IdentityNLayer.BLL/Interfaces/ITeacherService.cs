using IdentityNLayer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface ITeacherService : IBaseService<Teacher>
    {
        public Task<bool> HasAccount(string userId);
        public Task<Teacher> GetByUserId(string userId);
        public Task<IEnumerable<Group>> GetTeacherGroups(int teacherId);
    }
}
