using IdentityNLayer.Core.Entities;
using System.Collections.Generic;

namespace IdentityNLayer.Models
{
    public class CourseModel : Course
    {
        public List<StudentRequestsModel> StudentRequests;
        public int? AvailableGroupId { get; set; }
        
        public CourseFilterModel CourseFilter { get; set; }
    }
}
