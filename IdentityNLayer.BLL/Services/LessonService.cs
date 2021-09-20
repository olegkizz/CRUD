using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Services
{
    public class LessonService : ILessonService
    {
        private readonly IUnitOfWork Db;
        public LessonService(IUnitOfWork db)
        {
            Db = db;
        }
        public Task<int> CreateAsync(Lesson entity)
        {
            Db.Lessons.CreateAsync(entity);
            Db.Save();
            return Task.FromResult(entity.Id);
        }

        public async Task<EntityEntry<Lesson>> Delete(int id)
        {
            EntityEntry<Lesson> entry = await Db.Lessons.DeleteAsync(id);
            Db.Save();
            return entry;
        }

        public Task<bool> FileUseAsync(int fileId)
        {
            return Task.FromResult(Db.Lessons.Find(l => l.FileId == fileId).Any());
        }

        public Task<IEnumerable<Lesson>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Lesson> GetByIdAsync(int id)
        {
            return Db.Lessons.GetAsync(id);
        }

        public void UpdateAsync(Lesson entity)
        {
            Db.Lessons.UpdateAsync(entity);
            Db.Save();
        }
    }
}
