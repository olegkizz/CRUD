using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface IEnrollmentService
    {
        //public void Enrol(string userId, int entityId, UserRoles role, bool confirmed = true);
        public Task<int> EnrolInGroup(string userId, int groupId, UserRoles role);
        public Task<int?> EnrolInCourse(string userId, int courseId, UserRoles role);
        public void UnEnrol(string userId, int entityId);
    }
}
