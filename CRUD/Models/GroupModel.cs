using IdentityNLayer.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityNLayer.Models
{
    public class GroupModel
    {
        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        public GroupStatus Status { get; set; }
        public int? TeacherId { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public DateTime? StartDate { get; set; }

        public List<StudentRequestsModel> StudentRequests { get; set; }
        public List<Teacher> TeacherRequests { get; set; }
      /*  public void SetStudentRequests(IEnumerable<Enrollment> requests)
        {
            List<StudentRequestsModel> studentRequests = new();
            foreach (Enrollment request in requests)
            {
                if (requests != null)
                    studentRequests.Add(new StudentRequestsModel()
                    {
                        UserName = request.User.UserName,
                        UserId = request.UserID,
                        Applied = true
                    });
            }
            StudentRequests = studentRequests;
        }*/
    }
}
