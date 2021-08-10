using IdentityNLayer.Validation;
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
        [BirthDate(18, ErrorMessage = "Should be greater than 18")]
        public DateTime? BirthDate { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string? Phone { get; set; }
    }
}
