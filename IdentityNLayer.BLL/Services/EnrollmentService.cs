using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
using System;
using System.Linq;


namespace IdentityNLayer.BLL.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private IUnitOfWork Db { get; set; }

        public EnrollmentService(IUnitOfWork db)
        {
            Db = db;
        }
    
        //entityId - CourseId or GroupId, depends on #confirmed(true - groupId, false - courseId)
        public void Enrol(string userId, int entityId, UserRoles role, bool confirmed = true)
        {
            Group group = (Db.Groups.Find(gr => gr.Id == entityId))?.SingleOrDefault();

            Enrollment enrollment =
              Db.Enrollments.Find(en => en.UserID == userId && en.EntityID == (confirmed ? group?.CourseId : entityId)
              && en.Role == role)?.SingleOrDefault();
         
            if (enrollment?.State == UserGroupStates.Aborted || enrollment?.State == UserGroupStates.Requested)
            {
                enrollment.State = confirmed ? UserGroupStates.Applied : UserGroupStates.Requested;
                enrollment.EntityID = entityId;
                Db.Enrollments.UpdateAsync(enrollment);
            }
            else if (enrollment == null)
                Db.Enrollments.CreateAsync(new Enrollment
                {
                    UserID = userId,
                    EntityID = entityId,
                    Role = role,
                    State = confirmed ? UserGroupStates.Applied : UserGroupStates.Requested,
                    Updated = DateTime.Now
                });
            Db.Save();
        }

        public void UnEnrol(string userId, int groupdId)
        {
            Enrollment enrollment =
             Db.Enrollments.Find(en => en.UserID == userId && en.EntityID == groupdId && en.State == UserGroupStates.Applied)?.SingleOrDefault();
            if (enrollment != null)
            {
                enrollment.State = UserGroupStates.Aborted;
                Db.Enrollments.UpdateAsync(enrollment);
            }
            Db.Save();
        }
    }
}
