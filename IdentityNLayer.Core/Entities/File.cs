using Microsoft.AspNetCore.Http;


namespace IdentityNLayer.Core.Entities
{
    public class File
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public IFormFile FileContent { get; set; }
    }
}
