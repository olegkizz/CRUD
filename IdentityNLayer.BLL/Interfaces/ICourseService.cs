using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.Core.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface ICourseService : IBaseService<Course>
    {
        public Task<IEnumerable<Enrollment>> GetStudentRequests(int id);
        public Task<IEnumerable<Teacher>> GetTeacherRequests(int id);
        public Task<IEnumerable<Group>> GetGroups(int courseId, GroupStatus? status = null);
        public Task<bool> HasRequest(int courseId, string userId, UserRoles role);
        public Task<IEnumerable<Course>> Search(string search);
        Task<IEnumerable<Course>> Filter(CourseFilter courseFilter);
    }
}
