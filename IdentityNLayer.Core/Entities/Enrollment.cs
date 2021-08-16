using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityNLayer.Core.Entities
{

    public class Enrollment
    {
        public int Id { get; set; }
        /*[DisplayFormat(NullDisplayText = "No grade")]*/
        /*public Grade? Grade { get; set; }*/
        public DateTime Updated { get; set; }
        public int GroupID { get; set; }
        public virtual Group Group{ get; set; }
        public string UserID { get; set; }
        public virtual IdentityUser User { get; set; }
        public ActionsStudentGroup State{ get; set; }
        public UserRoles Role{ get; set; }
    }
}
