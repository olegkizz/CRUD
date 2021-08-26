using IdentityNLayer.Core.Entities;
using System.Collections.Generic;

namespace IdentityNLayer.Models
{
    public class CourseModel : Course
    {
        public List<StudentRequestsModel> StudentRequests;
        public int AvailableGroupId;
   /*     public void SetStudentRequests(IEnumerable<Enrollment> requests)
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
