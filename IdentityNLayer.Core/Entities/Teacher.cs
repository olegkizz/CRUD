using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace IdentityNLayer.Core.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Bio { get; set; }
        public string LinkToProfile { get; set; }
        public string UserId { get; set; }
        public Person User { get; set; }
    }
}
