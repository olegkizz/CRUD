using IdentityNLayer.Core.Entities;
using System;
using System.Collections.Generic;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface IGroupService : IBaseService<Group>
    {
        Teacher GetCurrentTeacher(int groupdId);
        public IEnumerable<Group> GetGroupsByUserId(string userId);
        public bool HasStudent(int groupId, string userId);
        public IEnumerable<Student> GetStudents(int? groupId, UserGroupStates? state = null);
        public IEnumerable<Teacher> GetTeachers(int? groupId, UserGroupStates? state = null);
    }
}
