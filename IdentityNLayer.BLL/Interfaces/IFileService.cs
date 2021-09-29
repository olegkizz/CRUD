using IdentityNLayer.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface IFileService : IBaseService<File>
    {
        public Task<File> GetByPathAsync(string path);
        public Task<File> CreateOrUpdateFileAsync(IFormFile file, string path);
    }
}
