using IdentityNLayer.Core.Entities;
using IdentityNLayer.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityNLayer.Models
{
    public class GroupLessonModel : GroupLesson
    {

        public new DateTime? StartDate { get; set; }
        public List<string> Error { get; set; } = new List<string>();
    }
}
