using IdentityNLayer.Core.Entities;
using System.Collections.Generic;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface IEnrollmentService
    {
        public void Enrol(string userId, int groupdId, UserRoles role, bool confirmed = true);
        public void UnEnrol(string userId, int groupdId);
        public IEnumerable<Enrollment> Get(string userId);
    }
}
