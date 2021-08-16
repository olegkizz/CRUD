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
        public void Enrol(string userId, int groupdId, UserRoles role, bool confirmed = true)
        {
            Enrollment enrollment =
               ((List<Enrollment>)Db.Enrollments.Find(en => en.UserID == userId && en.GroupID == groupdId)).FirstOrDefault();
            if (enrollment?.State == ActionsStudentGroup.Aborted)
            {
                enrollment.State = ActionsStudentGroup.Applied;
                Db.Enrollments.Update(enrollment);
            }
            else if (enrollment == null)
                Db.Enrollments.Create(new Enrollment
                {
                    UserID = userId,
                    GroupID = groupdId,
                    Role = role,
                    State = confirmed ? ActionsStudentGroup.Applied : ActionsStudentGroup.Requested,
                    Updated = DateTime.Now
                });
            Db.Save();
        }

        public void UnEnrol(string userId, int groupdId)
        {
            Enrollment enrollment =
             ((List<Enrollment>)Db.Enrollments.Find(en => en.UserID == userId && en.GroupID == groupdId)).FirstOrDefault();
            if (enrollment != null)
            {
                enrollment.State = ActionsStudentGroup.Aborted;
                Db.Enrollments.Update(enrollment);
            }
            Db.Save();
        }
    }
}
