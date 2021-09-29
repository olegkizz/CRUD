using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface ICourseService : IBaseService<Course>
    {
        public IEnumerable<Enrollment> GetStudentRequests(int id);
        public IEnumerable<Teacher> GetTeacherRequests(int id);
        public Task<IEnumerable<Topic>> GetAvailableTopicsAsync(int courseId = 0);
        public IEnumerable<Group> GetGroups(int courseId, GroupStatus? status = null);
        public bool HasRequest(int courseId, string userId, UserRoles role);
    }
}
