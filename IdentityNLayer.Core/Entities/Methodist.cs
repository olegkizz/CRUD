
namespace IdentityNLayer.Core.Entities
{
    public class Methodist
    {
        public int Id { get; set; }
        public string LinkToContact { get; set; }
        public string UserId { get; set; }
        public Person User { get; set; }
    }
}
