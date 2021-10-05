using System;
using System.Collections.Generic;

namespace IdentityNLayer.Core.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? TopicId { get; set; }
        public Topic Topic { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public DateTime Updated { get; set; }
    }
}
