using System.Collections.Generic;

namespace IdentityNLayer.Core.Entities
{
    public class Student : Person
    {
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public StudentType Type { get; set; }
    }
}