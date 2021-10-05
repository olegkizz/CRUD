using IdentityNLayer.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using static IdentityNLayer.Areas.Identity.Pages.Account.RegisterModel;

namespace IdentityNLayer.Models
{
    public class MethodistRegisterModel : Methodist
    {
        [RegularExpression(@"^((https|http)?:\/\/)?([\w-]{1,32}\.[\w-]{1,32})[^\s]*$", ErrorMessage = "Invalid link format.")]
        public new string LinkToContact { get; set; }
        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string Error { get; set; }
    }
}
