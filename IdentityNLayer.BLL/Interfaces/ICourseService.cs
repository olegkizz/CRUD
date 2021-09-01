using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using System.Collections.Generic;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface ICourseService : IBaseService<Course>
    {
        public IEnumerable<Enrollment> GetStudentRequests(int id);
        public IEnumerable<Teacher> GetTeacherRequests(int id);
        public IEnumerable<Topic> GetAvailableTopics();
        public IEnumerable<Group> GetAvailableGroups(int courseId);
        public bool HasRequest(int courseId, string userId, UserRoles role);
    }
}
