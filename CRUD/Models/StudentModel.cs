using IdentityNLayer.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Identity;
namespace IdentityNLayer.Models
{
    public class StudentModel : Student
    {
        public string[] StudentTypes { get; set; }
        public string Name
        {
            get
            {
                return User?.FirstName + " " + User?.LastName;
            }
        }
        public StudentModel()
        {
            StudentTypes = Enum.GetNames(typeof(StudentType));
        }
    }
}
