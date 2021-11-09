using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
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

        public async Task<int?> EnrolInCourse(string userId, int courseId, UserRole role)
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
                    State = UserGroupState.Requested,
                    Updated = DateTime.Now
                };
                await Db.Enrollments.CreateAsync(enrollment);
                await Db.Save();
            } else if (enrollment.State == UserGroupState.Aborted)
            {
                enrollment.State = UserGroupState.Requested;
                Db.Enrollments.Update(enrollment);
                await Db.Save();
            }
            return enrollment?.Id;
        }

        public async Task<int> EnrolInGroup(string userId, int groupId, UserRole role)
        {
            int courseId = (await Db.Groups.FindAsync(gr => gr.Id == groupId)).SingleOrDefault().CourseId;

            Enrollment enrollment =
              (await Db.Enrollments.FindAsync(en => en.UserID == userId && en.EntityID == courseId
              && en.Role == role && en.State == UserGroupState.Requested)).SingleOrDefault();

            if (enrollment == null)
            {
                enrollment = new Enrollment
                {
                    UserID = userId,
                    EntityID = groupId,
                    Role = role,
                    State = UserGroupState.Applied,
                    Updated = DateTime.Now
                };
                await Db.Enrollments.CreateAsync(enrollment);
            }
            else if (enrollment.State == UserGroupState.Aborted || enrollment.State == UserGroupState.Requested)
            {
                enrollment.State = UserGroupState.Applied;
                enrollment.EntityID = groupId;
                Db.Enrollments.Update(enrollment);
            }
            await Db .Save();

            return enrollment.Id;
        }


        public async Task UnEnrol(string userId, int groupId)
        {
            int courseId = (await Db.Groups.FindAsync(gr => gr.Id == groupId)).SingleOrDefault().CourseId;

            Enrollment enrollment =
             (await Db.Enrollments.FindAsync(en => en.UserID == userId && en.EntityID == groupId
                    && en.State == UserGroupState.Applied)).SingleOrDefault();
            if (enrollment == null)
            {
                return;
            }
            enrollment.State = UserGroupState.Aborted;
            enrollment.EntityID = courseId;
            Db.Enrollments.Update(enrollment);
            await Db.Save();
        }

        public async Task CancelRequest(string userId, int courseId)
        {
            Enrollment enrollment =
            (await Db.Enrollments.FindAsync(en => en.UserID == userId && en.EntityID == courseId
                   && en.State == UserGroupState.Requested)).SingleOrDefault();
            if (enrollment == null)
            {
                return;
            }
            await Db.Enrollments.DeleteAsync(enrollment.Id);
            await Db.Save();
        }
    }
}
