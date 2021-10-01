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
        public UserGroupStates State{ get; set; }
        public UserRoles Role{ get; set; }
    }
}
