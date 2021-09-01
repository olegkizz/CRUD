using IdentityNLayer.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityNLayer.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        /*[BirthDate(18, ErrorMessage = "Should be greater than 18")]*/
        public DateTime? BirthDate { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public string Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        /*[Compare("User.PasswordHash", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }*/
    }
}
