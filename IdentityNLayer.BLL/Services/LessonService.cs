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
        public async Task<int> CreateAsync(Lesson entity)
        {
            await Db.Lessons.CreateAsync(entity);
            await Db.Save();
            return entity.Id;
        }

        public async Task Delete(int id)
        {
            await Db.Lessons.DeleteAsync(id);
            await Db .Save();
        }

        public async Task<bool> FileUseAsync(int fileId)
        {
            return (await Db.Lessons.FindAsync(l => l.FileId == fileId)).Any();
        }

        public Task<IEnumerable<Lesson>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Lesson> GetByIdAsync(int id)
        {
            return Db.Lessons.GetAsync(id);
        }

        public async Task UpdateAsync(Lesson entity)
        {
            Db.Lessons.Update(entity);
            await Db.Save();
        }
    }
}
