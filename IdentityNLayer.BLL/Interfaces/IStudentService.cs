using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityNLayer.Core.Entities;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface IStudentService : IBaseService<Student>
    {
        public Array GetStudentTypes();
        public Task<List<Group>> GetStudentGroupsAsync(int studentId);
        public Task<bool> HasAccount(string userId);
        public Task<Student> GetByUserId(string userId);
        public Task<Group> GetGroupByCourseIdAsync(int studentId, int courseId);
    }
}
