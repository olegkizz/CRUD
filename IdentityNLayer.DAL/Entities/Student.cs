namespace IdentityNLayer.DAL.Entities
{
    public class Student : Person
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public StudentType Type { get; set; }
    }
}