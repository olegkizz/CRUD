using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using IdentityNLayer.DAL.Entities;

namespace IdentityNLayer.BLL.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int GroupId { get; set; }
        public StudentType Type { get; set; }
    }
}