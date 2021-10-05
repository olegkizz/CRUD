using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityNLayer.Models
{
    public class CourseFilterModel
    {
        public string TitleContains { get; set; }
        public string DescriptionContains { get; set; }
        public string TopicTitleContains { get; set; }
        [Display(Name = "Lessons Should Be From")]
        public int? LessonsFrom { get; set; }
        public int? LessonsTo { get; set; }
    }
}
