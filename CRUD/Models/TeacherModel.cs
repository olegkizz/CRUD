using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityNLayer.Models
{
    public class TeacherModel : PersonModel
    {
        public string Bio { get; set; }
        [RegularExpression(@"^((https|http)?:\/\/)?([\w-]{1,32}\.[\w-]{1,32})[^\s]*$", ErrorMessage="Invalid link format")]
        public string LinkToProfile { get; set; }
    }
}
