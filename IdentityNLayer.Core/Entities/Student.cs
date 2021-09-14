using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace IdentityNLayer.Core.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public StudentType Type { get; set; }
        public string UserId { get; set; }
        public Person User { get; set; }
    }
}