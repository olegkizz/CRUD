using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface IEnrollmentService
    {
        public Task<int> EnrolInGroup(string userId, int groupId, UserRole role);
        public Task<int?> EnrolInCourse(string userId, int courseId, UserRole role);
        public Task UnEnrol(string userId, int entityId);
        public Task CancelRequest(string userId, int courseId);
    }
}
