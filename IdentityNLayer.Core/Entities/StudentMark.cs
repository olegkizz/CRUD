using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNLayer.Core.Entities
{
    public class StudentMark
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public int? Mark { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
