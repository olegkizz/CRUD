using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Services
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork Db;
        public FileService(IUnitOfWork db)
        {
            Db = db;
        }
        public Task<int> CreateAsync(File entity)
        {
            Db.Files.CreateAsync(entity);
            Db.Save();
            return Task.FromResult(entity.Id);
        }

        public async Task<File> CreateOrUpdateFileAsync(IFormFile file, string path)
        {
            File newFile = null;
           
            System.IO.Directory.CreateDirectory(path);

            path = path + "/" + file.FileName;
            using (System.IO.FileStream inputStream = new(path, System.IO.FileMode.Create))
            {
                // read file to stream
                await file.CopyToAsync(inputStream);
                // stream to byte array
                byte[] array = new byte[inputStream.Length];
                inputStream.Seek(0, System.IO.SeekOrigin.Begin);
                inputStream.Read(array, 0, array.Length);
                newFile = new() { Name = file.FileName, ContentType = file.ContentType, FileContent = array, Path = path};
            }
            var oldFile = await GetByPathAsync(path);
            if (oldFile?.Path == newFile.Path && oldFile != null)
            {
                newFile.Id = oldFile.Id;
                UpdateAsync(newFile);
            }
            else await CreateAsync(newFile);
            return newFile;
        }

        public async Task<EntityEntry<File>> Delete(int id)
        {
            System.IO.File.Delete((await GetByIdAsync(id)).Path);
            EntityEntry<File> entry = await Db.Files.DeleteAsync(id);
            Db.Save();
            return entry;
        }

        public Task<IEnumerable<File>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<File> GetByIdAsync(int id)
        {
            return Db.Files.GetAsync(id);
        }

        public async Task<File> GetByPathAsync(string path)
        {
            return (await Db.Files.FindAsync(f => f.Path == path)).SingleOrDefault();
        }

        public void UpdateAsync(File entity)
        {
            Db.Files.UpdateAsync(entity);
            Db.Save();
        }
    }
}
