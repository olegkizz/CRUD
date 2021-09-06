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

        public IEnumerable<Teacher> GetAll()
        {
            return Db.Teachers.GetAll();
        }
        public Teacher GetById(int id)
        { 
            return Db.Teachers.Get(id);
        }

        public int Create(Teacher entity)
        {
            Db.Teachers.Create(entity);
            Db.Save();
            return entity.Id;
        }

        public void Update(Teacher entity)
        {
            throw new NotImplementedException();
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
            return Db.Teachers.Find(tc => tc.UserId == userId).FirstOrDefault();
        }

        public IEnumerable<Group> GetTeacherGroups(int teacherId)
        {
            List<Group> groups = new();
            foreach (Enrollment en in Db.Enrollments.Find(en => en.UserID == Db.Teachers.Get(teacherId).UserId))
            {
                if (en.State != UserGroupStates.Aborted && en.State != UserGroupStates.Requested)
                    groups.Add(Db.Groups.Get(en.EntityID));
            }
            return groups;
        }

        public int CreateAsync(Teacher entity)
        {
            throw new NotImplementedException();
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
