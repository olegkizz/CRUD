
using System;

namespace IdentityNLayer.Core.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Theme { get; set; }
        public int? FileId { get; set; }
        public File File { get; set; }
        public int CourseId { get; set; }
        public Course Course{ get; set; }
        public DateTime? Updated{ get; set; }
    }
}
