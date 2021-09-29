using IdentityNLayer.Core.Entities;
using IdentityNLayer.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityNLayer.Models
{
    public class LessonModel
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Theme { get; set; }
        [Range(1, 60, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Duration { get; set; }
        public int? FileId { get; set; }
        public File File { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
