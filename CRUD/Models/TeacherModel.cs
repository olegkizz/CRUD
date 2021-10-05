using IdentityNLayer.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityNLayer.Models
{
    public class TeacherModel : Teacher
    {
        [RegularExpression(@"^((https|http)?:\/\/)?([\w-]{1,32}\.[\w-]{1,32})[^\s]*$", ErrorMessage="Invalid link format.")]
        public new string LinkToProfile { get; set; }
        public string Name
        {
            get
            {
                return User?.FirstName + " " + User?.LastName;
            }
        }
    }
}
