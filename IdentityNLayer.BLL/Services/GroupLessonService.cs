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
    public class GroupLessonService : IGroupLessonService
    {
        private readonly IUnitOfWork Db;
        public GroupLessonService(IUnitOfWork db)
        {
            Db = db;
        }
        public Task<int> CreateAsync(GroupLesson entity)
        {
            Db.GroupLessons.CreateAsync(entity);
            Db.Save();
            return Task.FromResult(entity.Id);
        }

        public Task<EntityEntry<GroupLesson>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GroupLesson>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GroupLesson> GetByIdAsync(int id)
        {
            return Db.GroupLessons.GetAsync(id);
        }

        public Task<GroupLesson> GetByLessonAndGroupIdAsync(int groupId, int lessonId)
        {
            return Task.FromResult(Db.GroupLessons.Find(gl => gl.GroupId == groupId && gl.LessonId == lessonId).FirstOrDefault());
        }

        public Task<List<GroupLesson>> GetLessonsByGroupIdAsync(int groupId)
        {
            return Task.FromResult(Db.GroupLessons.Find(gl => gl.GroupId == groupId).OrderBy(gl => gl.StartDate).ToList());
        }

        public void UpdateAsync(GroupLesson entity)
        {
            Db.GroupLessons.UpdateAsync(entity);
            Db.Save();
        }
    }
}
