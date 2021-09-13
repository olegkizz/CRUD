using System;
using System.Collections.Generic;
using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Services
{
    public class TeacherService : ITeacherService
    {
        private IUnitOfWork Db { get; set; }
        public TeacherService(IUnitOfWork db)
        {
            Db = db;
        }

        public void Update(Teacher entity)
        {
            Db.Teachers.Update(entity);
            Db.Save();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool HasAccount(string userId)
        {
            return Db.Teachers.Find(tc => tc.UserId == userId).Any();
        }

        public Teacher GetByUserId(string userId)
        {
            return Db.Teachers.Find(tc => tc.UserId == userId).SingleOrDefault();
        }

        public IEnumerable<Group> GetTeacherGroups(int teacherId)
        {
            List<Group> groups = new();

            Task.Factory.StartNew(async () =>
            {
                Teacher teacher = await Db.Teachers.GetAsync(teacherId);
                foreach (Enrollment en in Db.Enrollments.Find(en => en.UserID == teacher.UserId))
                {
                    if (en.State != UserGroupStates.Aborted && en.State != UserGroupStates.Requested)
                    {
                        Group group = await Db.Groups.GetAsync(en.EntityID);
                        groups.Add(group);
                    }
                }
            });
            return groups;
        }

        public int CreateAsync(Teacher entity)
        {
            Db.Teachers.CreateAsync(entity);
            Db.Save();
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
