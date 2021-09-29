using IdentityNLayer.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityNLayer.Models
{
    public class StudentMarkModel : StudentMark
    {
        [Range(1, 10, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public new int? Mark { get; set; }
    }
}
