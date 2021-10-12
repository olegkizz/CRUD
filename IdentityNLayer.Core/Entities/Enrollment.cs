using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityNLayer.Core.Entities
{

    public class Enrollment
    {
        public int Id { get; set; }
        public DateTime Updated { get; set; }
        public int EntityID { get; set; }
        public string UserID { get; set; }
        public Person User { get; set; }
        public UserGroupState State{ get; set; }
        public UserRole Role{ get; set; }
    }
}
