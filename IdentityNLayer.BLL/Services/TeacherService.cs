using System;
using System.Collections.Generic;
using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace IdentityNLayer.BLL.Services
{
    public class TeacherService : ITeacherService
    {
        private IUnitOfWork Db { get; set; }
        public TeacherService(IUnitOfWork db)
        {
            Db = db;
        }

        public async Task UpdateAsync(Teacher entity)
        {
            Db.Teachers.Update(entity);
            await Db.Save();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> HasAccount(string userId)
        {
            return (await Db.Teachers.FindAsync(tc => tc.UserId == userId)).Any();
        }

        public async Task<Teacher> GetByUserId(string userId)
        {
            return (await Db.Teachers.FindAsync(tc => tc.UserId == userId)).SingleOrDefault();
        }

        public async Task<IEnumerable<Group>> GetTeacherGroups(int teacherId)
        {
            List<Group> groups = new();

            Teacher teacher = await Db.Teachers.GetAsync(teacherId);
            foreach (Enrollment en in await Db.Enrollments.FindAsync(en => en.UserID == teacher.UserId))
            {
                if (en.State != UserGroupState.Aborted && en.State != UserGroupState.Requested)
                {
                    Group group = await Db.Groups.GetAsync(en.EntityID);
                    groups.Add(group);
                }
            }

            return groups;
        }

        public async Task<int> CreateAsync(Teacher entity)
        {
            await Db.Teachers.CreateAsync(entity);
            await Db.Save();
            return entity.Id;
        }

        public Task<Teacher> GetByIdAsync(int id)
        {
            return Db.Teachers.GetAsync(id);
        }

        public Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return Db.Teachers.GetAllAsync();
        }
    }
}
