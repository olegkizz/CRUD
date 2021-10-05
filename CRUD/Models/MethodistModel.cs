using IdentityNLayer.Core.Entities;
using IdentityNLayer.Validation;
using System.ComponentModel.DataAnnotations;

namespace IdentityNLayer.Models
{
    public class MethodistModel : Methodist
    {
        [RegularExpression(@"^((https|http)?:\/\/)?([\w-]{1,32}\.[\w-]{1,32})[^\s]*$", ErrorMessage = "Invalid link format.")]
        public new string LinkToContact { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Name
        {
            get
            {
                return User?.FirstName + " " + User?.LastName;
            }
        }
    }
}
