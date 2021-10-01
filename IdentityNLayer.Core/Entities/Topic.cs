using System.ComponentModel.DataAnnotations;


namespace IdentityNLayer.Core.Entities
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public Topic Parent { get; set; }
    }
}
