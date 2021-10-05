using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityNLayer.Core.Entities
{
    public class Group
    {
        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        public GroupStatus Status { get; set; }
        public int? TeacherId { get; set; }
        public Teacher Teacher{ get; set; }
        public int? MethodistId { get; set; }
        public Methodist Methodist { get; set; }
        public DateTime StartDate { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}