using System;
using System.Collections.Generic;
using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Services
{
    public class GroupService : IGroupService
    {
        private IUnitOfWork Db { get; set; }
        public GroupService(IUnitOfWork db)
        {
            Db = db;
        }
        public IEnumerable<Group> GetAll()
        {
            return Db.Groups.GetAll();
        }
        public Group GetById(int id)
        {
            return Db.Groups.Get(id);
        }

        public int Create(Group entity)
        {
            Db.Groups.Create(entity);
            Db.Save();
            return entity.Id;
        }

        public void Update(Group entity)
        {
            Db.Groups.Update(entity);
            Db.Save();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Teacher GetCurrentTeacher(int groupId)
        {
            return Db.Groups.Find(gr => gr.Id == groupId).FirstOrDefault()?.Teacher;
        }

        public IEnumerable<Group> GetGroupsByUserId(string userId)
        {
            List<Group> groups = new();
            foreach (Enrollment en in Db.Enrollments.Find(en => en.UserID == userId))
            {
                if (en.State != UserGroupStates.Requested && en.State != UserGroupStates.Aborted)
                    groups.Add(Db.Groups.Get(en.EntityID));
            }
            return groups;
        }

        public IEnumerable<Student> GetStudents(int? groupId, UserGroupStates? state = null)
        {
            if (groupId == null)
                return new List<Student>();

            List<Student> students = new ();
            switch (state)
            {
                case UserGroupStates.Applied:
                    foreach (Enrollment en in Db.Enrollments.Find(en => en.EntityID == groupId && en.State == state && en.Role == UserRoles.Student))
                    {
                        students.Add(Db.Students.Find(st => st.UserId == en.UserID).FirstOrDefault());
                    }
                    return students;
                case UserGroupStates.Requested:
                    foreach (Enrollment en in Db.Enrollments.Find(en => en.EntityID == Db.Groups.Find(gr => gr.Id == groupId).FirstOrDefault()?.CourseId 
                    && en.State == state && en.Role == UserRoles.Student))
                    {
                        students.Add(Db.Students.Find(st => st.UserId == en.UserID).FirstOrDefault());
                    }
                    return students;
                default:
                    return GetStudents(groupId, UserGroupStates.Applied).Concat(GetStudents(groupId, UserGroupStates.Requested));
            }
        }

        public IEnumerable<Teacher> GetTeachers(int? groupId, UserGroupStates? state = null)
        {
            if (groupId == null)
                return new List<Teacher>();
            
            List<Teacher> teachers = new();
            switch (state)
            {
                case UserGroupStates.Applied:
                    foreach (Enrollment en in Db.Enrollments.Find(en => en.EntityID == groupId && en.State == state && en.Role == UserRoles.Teacher))
                    {
                        teachers.Add(Db.Teachers.Find(tc => tc.UserId == en.UserID).FirstOrDefault());
                    }
                    return teachers;
                case UserGroupStates.Requested:
                    foreach (Enrollment en in Db.Enrollments.Find(en => en.EntityID == Db.Groups.Find(gr => gr.Id == groupId).FirstOrDefault()?.CourseId
                    && en.State == state && en.Role == UserRoles.Teacher))
                    {
                        teachers.Add(Db.Teachers.Find(tc => tc.UserId == en.UserID).FirstOrDefault());
                    }
                    return teachers;
                default:
                    return GetTeachers(groupId, UserGroupStates.Applied).Concat(GetTeachers(groupId, UserGroupStates.Requested));
            }
        }

        public bool HasStudent(int groupId, string userId)
        {
            return Db.Enrollments.Find(en => en.UserID == userId 
            && en.Role == UserRoles.Student 
            && en.EntityID == groupId
            && en.State == UserGroupStates.Applied)
                .FirstOrDefault() != null
                ? true : false;
        }

        public int CreateAsync(Group entity)
        {
            throw new NotImplementedException();
        }

        public string GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Group> GetByIdAsync(int id)
        {
            return Db.Groups.GetAsync(id);
        }

        public Task<IEnumerable<Group>> GetAllAsync()
        {
            return Db.Groups.GetAllAsync();
        }
    }
}
