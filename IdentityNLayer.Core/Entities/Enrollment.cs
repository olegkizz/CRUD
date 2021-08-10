using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityNLayer.Core.Entities
{

    public class Enrollment
    {
        public int Id { get; set; }
        /*[DisplayFormat(NullDisplayText = "No grade")]*/
        /*public Grade? Grade { get; set; }*/
        public DateTime Created { get; set; }
        public int GroupID { get; set; }
        public virtual Group Group{ get; set; }
        public int StudentID { get; set; }
        public virtual Student Student { get; set; }
        public ActionsStudentGroup State{ get; set; }
    }
}
