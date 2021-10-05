using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNLayer.Core.Filters
{
    public class CourseFilter
    {
        public string TitleContains { get; set; }
        public string DescriptionContains { get; set; }
        public string TopicTitleContains { get; set; }
        public int? LessonsFrom { get; set; }
        public int? LessonsTo { get; set; }
    }
}
