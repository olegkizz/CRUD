using IdentityNLayer.Core.Entities;
using System;
using System.Collections.Generic;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface ITeacherService : IBaseService<Teacher>
    {
        public bool HasAccount(string userId);
        public Teacher GetByUserId(string userId);
        public IEnumerable<Group> GetTeacherGroups(int teacherId);
    }
}
