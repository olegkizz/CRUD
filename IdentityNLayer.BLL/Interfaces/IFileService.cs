using IdentityNLayer.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface IFileService : IBaseService<File>
    {
        public Task<File> GetByPathAsync(string path);
        public Task<File> CreateOrUpdateFileAsync(IFormFile file, string path);
    }
}
