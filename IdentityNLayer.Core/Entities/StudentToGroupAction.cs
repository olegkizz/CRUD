using System;

namespace IdentityNLayer.Core.Entities
{
    public class StudentToGroupAction
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public ActionsStudentGroup Action { get; set; }
        public DateTime Date { get; set; }
    }
}
