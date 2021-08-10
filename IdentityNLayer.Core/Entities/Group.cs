using System;
using System.Collections.Generic;

namespace IdentityNLayer.Core.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public GroupStatus Status { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher{ get; set; }
        public DateTime StartDate { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }

    }
}