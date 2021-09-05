using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private IUnitOfWork Db { get; set; }

        public EnrollmentService(IUnitOfWork db)
        {
            Db = db;

        }
        public void Enrol(string userId, int groupId, UserRoles role, bool confirmed = true)
        {
            Group group = Db.Groups.Find(gr => gr.Id == groupId).FirstOrDefault();
            Enrollment enrollment =
              Db.Enrollments.Find(en => en.UserID == userId && en.EntityID == (group != null ? group.CourseId : groupId) 
              && en.Role == role).FirstOrDefault();
            if (enrollment?.State == UserGroupStates.Aborted || enrollment?.State == UserGroupStates.Requested)
            {
                enrollment.State = confirmed ? UserGroupStates.Applied : UserGroupStates.Requested;
                enrollment.EntityID = groupId;
                Db.Enrollments.Update(enrollment);
            }
            else if (enrollment == null)
                Db.Enrollments.Create(new Enrollment
                {
                    UserID = userId,
                    EntityID = groupId,
                    Role = role,
                    State = confirmed ? UserGroupStates.Applied : UserGroupStates.Requested,
                    Updated = DateTime.Now
                });
            Db.Save();
        }

        public void UnEnrol(string userId, int groupdId)
        {
            Enrollment enrollment =
             Db.Enrollments.Find(en => en.UserID == userId && en.EntityID == groupdId).FirstOrDefault();
            if (enrollment != null)
            {
                enrollment.State = UserGroupStates.Aborted;
                Db.Enrollments.Update(enrollment);
            }
            Db.Save();
        }
        public IEnumerable<Enrollment> Get(string userId)
        {
            return Db.Enrollments.Find(en => en.UserID == userId);
        }
    }
}
