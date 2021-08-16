using IdentityNLayer.Core.Entities;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface IEnrollmentService
    {
        public void Enrol(string userId, int groupdId, UserRoles role, bool confirmed = true);
        public void UnEnrol(string userId, int groupdId);
    }
}
