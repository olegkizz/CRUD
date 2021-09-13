using IdentityNLayer.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace IdentityNLayer.Models
{
    public class StudentModel : PersonModel
    {
        public string Type { get; set; }
        /*   public List<AssignGroupModel> AssignGroups { get; set; }*/
        public string[] StudentTypes { get; set; }
        public StudentModel()
        {
            StudentTypes = Enum.GetNames(typeof(StudentType));
        }
    }
}
