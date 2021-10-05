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
        public async Task<int> CreateAsync(GroupLesson entity)
        {
            await Db.GroupLessons.CreateAsync(entity);
            await Db.Save();
            return entity.Id;
        }

        public Task Delete(int id)
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

        public async Task<GroupLesson> GetByLessonAndGroupIdAsync(int groupId, int lessonId)
        {
            return (await Db.GroupLessons.FindAsync(gl => gl.GroupId == groupId && gl.LessonId == lessonId)).SingleOrDefault();
        }

        public async Task<List<GroupLesson>> GetLessonsByGroupIdAsync(int groupId)
        {
            return (await Db.GroupLessons.FindAsync(gl => gl.GroupId == groupId)).OrderBy(gl => gl.StartDate).ToList();
        }

        public async Task UpdateAsync(GroupLesson entity)
        {
            Db.GroupLessons.Update(entity);
            await Db.Save();
        }
    }
}
