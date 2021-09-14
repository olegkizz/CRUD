using System;

namespace IdentityNLayer.Core.Entities
{
    public class GroupLesson
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
