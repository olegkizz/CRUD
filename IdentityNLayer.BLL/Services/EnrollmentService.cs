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
              Db.Enrollments.Find(en => en.UserID == userId && en.EntityID == (group != null ? group.CourseId : groupId)).FirstOrDefault();
            if (enrollment?.State == ActionsStudentGroup.Aborted || enrollment?.State == ActionsStudentGroup.Requested)
            {
                enrollment.State = ActionsStudentGroup.Applied;
                enrollment.EntityID = groupId;
                Db.Enrollments.Update(enrollment);
            }
            else if (enrollment == null)
                Db.Enrollments.Create(new Enrollment
                {
                    UserID = userId,
                    EntityID = groupId,
                    Role = role,
                    State = confirmed ? ActionsStudentGroup.Applied : ActionsStudentGroup.Requested,
                    Updated = DateTime.Now
                });
            Db.Save();
        }

        public void UnEnrol(string userId, int groupdId)
        {
            Enrollment enrollment =
             ((List<Enrollment>)Db.Enrollments.Find(en => en.UserID == userId && en.EntityID == groupdId)).FirstOrDefault();
            if (enrollment != null)
            {
                enrollment.State = ActionsStudentGroup.Aborted;
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
