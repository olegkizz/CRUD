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
    public class CourseService : ICourseService
    {
        private IUnitOfWork Db { get; set; }

        public CourseService(IUnitOfWork db)
        {
            Db = db;
        }

        public Task<EntityEntry<Course>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(Course entity)
        {
            Db.Courses.UpdateAsync(entity);
            Db.Save();
        }
        public async Task<IEnumerable<Enrollment>> GetStudentRequests(int id)
        {
            return await Db.Enrollments.FindAsync(en => en.EntityID == id && en.State == UserGroupStates.Requested && en.Role == UserRoles.Student);
        }
        public async Task<IEnumerable<Teacher>> GetTeacherRequests(int id)
        {
            List<Teacher> teachers = new();
            foreach (Enrollment en
                in await Db.Enrollments.FindAsync(en => en.EntityID == id 
                    && en.State == UserGroupStates.Requested && en.Role == UserRoles.Teacher))
            {
                Teacher teacher = (await Db.Teachers.FindAsync(tc => tc.UserId == en.UserID)).SingleOrDefault();
                if (teacher != null)
                    teachers.Add(teacher);
            }
            return teachers;
        }

        public async Task<bool> HasRequest(int courseId, string userId, UserRoles role)
        {
            return (await Db.Enrollments.FindAsync(en => en.EntityID == courseId 
            && en.UserID == userId 
            && en.Role == role 
            && en.State == UserGroupStates.Requested))
                .Any();
        }

        public async Task<IEnumerable<Group>> GetGroups(int courseId, GroupStatus? status = null)
        {
            List<Group> groups = new();
            if (status != null)
                groups = (await Db.Groups.FindAsync(gr => gr.CourseId == courseId && gr.Status == status)).ToList();
            else
            {
                foreach(GroupStatus statusTemp in Enum.GetValues<GroupStatus>())
                {
                    List<Group> group = (await Db.Groups.FindAsync(gr => gr.CourseId == courseId
                            && gr.Status == statusTemp)).ToList();
                    groups = groups.Concat(group).ToList();
                }
            }
            return groups;
        }

        public Task<int> CreateAsync(Course entity)
        {
            Db.Courses.CreateAsync(entity);
            Db.Save();
            return Task.FromResult(entity.Id);
        }

        public Task<Course> GetByIdAsync(int id)
        {
            return Db.Courses.GetAsync(id);

        }

        public Task<IEnumerable<Course>> GetAllAsync()
        {
            return Db.Courses.GetAllAsync();
        }

    }
}
