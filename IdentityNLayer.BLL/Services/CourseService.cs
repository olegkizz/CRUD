using IdentityNLayer.BLL.Extensions;
using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.Core.Filters;
using IdentityNLayer.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
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

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Course entity)
        {
            Db.Courses.Update(entity);
            await Db.Save();
        }
        public async Task<IEnumerable<Enrollment>> GetStudentRequests(int id)
        {
            return await Db.Enrollments.FindAsync(en => en.EntityID == id && en.State == UserGroupState.Requested && en.Role == UserRole.Student);
        }
        public async Task<IEnumerable<Teacher>> GetTeacherRequests(int id)
        {
            List<Teacher> teachers = new();
            foreach (Enrollment en
                in await Db.Enrollments.FindAsync(en => en.EntityID == id 
                    && en.State == UserGroupState.Requested && en.Role == UserRole.Teacher))
            {
                Teacher teacher = (await Db.Teachers.FindAsync(tc => tc.UserId == en.UserID)).SingleOrDefault();
                if (teacher != null)
                    teachers.Add(teacher);
            }
            return teachers;
        }

        public async Task<bool> HasRequest(int courseId, string userId, UserRole role)
        {
            return (await Db.Enrollments.FindAsync(en => en.EntityID == courseId 
            && en.UserID == userId 
            && en.Role == role 
            && en.State == UserGroupState.Requested))
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

        public async Task<int> CreateAsync(Course entity)
        {
            await Db.Courses.CreateAsync(entity);
            await Db.Save();
            return entity.Id;
        }

        public Task<Course> GetByIdAsync(int id)
        {
            return Db.Courses.GetAsync(id);

        }

        public Task<IEnumerable<Course>> GetAllAsync()
        {
            return Db.Courses.GetAllAsync();
        }

        public async Task<IEnumerable<Course>> Search(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return await GetAllAsync();

            return (await GetAllAsync()).Where(c =>
               c.Title.Contains(search.NormalizeSearchString(), StringComparison.OrdinalIgnoreCase) ||
               c.Description.Contains(search.NormalizeSearchString(), StringComparison.OrdinalIgnoreCase) ||
               c.Topic.Title.Contains(search.NormalizeSearchString(), StringComparison.OrdinalIgnoreCase));
        }

        public async Task<IEnumerable<Course>> Filter(CourseFilter courseFilter)
        {
            return await Db.Courses.Filter(courseFilter);
        }
    }
}
