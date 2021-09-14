using Microsoft.AspNetCore.Identity;
using System;

namespace IdentityNLayer.Core.Entities
{
    public class Person : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}