using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
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

        public async Task<int?> EnrolInCourse(string userId, int courseId, UserRoles role)
        {
            Enrollment enrollment =
              (await Db.Enrollments.FindAsync(en => en.UserID == userId && en.EntityID == courseId
              && en.Role == role)).SingleOrDefault();

            if (enrollment == null)
            {
                enrollment = new Enrollment
                {
                    UserID = userId,
                    EntityID = courseId,
                    Role = role,
                    State = UserGroupStates.Requested,
                    Updated = DateTime.Now
                };
                Db.Enrollments.CreateAsync(enrollment);
                Db.Save();
            }
            return enrollment?.Id;
        }

        public async Task<int> EnrolInGroup(string userId, int groupId, UserRoles role)
        {
            Enrollment enrollment =
              (await Db.Enrollments.FindAsync(en => en.UserID == userId && en.EntityID == groupId
              && en.Role == role)).SingleOrDefault();

            if (enrollment == null)
            {
                enrollment = new Enrollment
                {
                    UserID = userId,
                    EntityID = groupId,
                    Role = role,
                    State = UserGroupStates.Applied,
                    Updated = DateTime.Now
                };
                Db.Enrollments.CreateAsync(enrollment);
            }
            else if (enrollment.State == UserGroupStates.Aborted || enrollment.State == UserGroupStates.Requested)
            {
                enrollment.State = UserGroupStates.Applied;
                enrollment.EntityID = groupId;
                Db.Enrollments.UpdateAsync(enrollment);
            }
            Db.Save();

            return enrollment.Id;
        }

        //entityId - CourseId or GroupId, depends on #confirmed(true - groupId, false - courseId)
        /*     public async void Enrol(string userId, int entityId, UserRoles role, bool confirmed = true)
             {
                 Group group = (await Db.Groups.FindAsync(gr => gr.Id == entityId)).SingleOrDefault();

                 Enrollment enrollment =
                   (await Db.Enrollments.FindAsync(en => en.UserID == userId && en.EntityID == (confirmed ? group?.CourseId : entityId)
                   && en.Role == role)).SingleOrDefault();

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
             }*/

        public async void UnEnrol(string userId, int groupdId)
        {
            Enrollment enrollment =
             (await Db.Enrollments.FindAsync(en => en.UserID == userId && en.EntityID == groupdId
                    && en.State == UserGroupStates.Applied)).SingleOrDefault();
            if (enrollment == null)
            {
                return;
            }
            enrollment.State = UserGroupStates.Aborted;
            Db.Enrollments.UpdateAsync(enrollment);
            Db.Save();
        }
    }
}
