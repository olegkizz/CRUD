using System;
using System.Collections.Generic;
using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace IdentityNLayer.BLL.Services
{
    public class GroupService : IGroupService
    {
        private IUnitOfWork Db { get; set; }
        private IStudentMarkService _studentMarkService{ get; set; }
        private UserManager<Person> _userManager{ get; set; }
        public GroupService(IUnitOfWork db,
            IStudentMarkService studentMarkService,
             UserManager<Person> userManager)
        {
            Db = db;
            _studentMarkService = studentMarkService;
            _userManager = userManager;
        }

        public async Task  UpdateAsync(Group entity)
        {
            Db.Groups.Update(entity);
            await Db.Save();
        }

        public async Task Delete(int id)
        {
            await Db.Groups.DeleteAsync(id);
            await Db.Save();
        }

        public async Task<Teacher> GetCurrentTeacher(int groupId)
        {
            return (await Db.Groups.FindAsync(gr => gr.Id == groupId)).SingleOrDefault()?.Teacher;
        }

        public async Task<IEnumerable<Group>> GetGroupsByUserIdAsync(string userId)
        {
            List<Group> groups = new();
            foreach(string role in await _userManager.GetRolesAsync
                (await _userManager.FindByIdAsync(userId)))
            {
                if (role == "Admin")
                    groups = (await GetAllAsync()).ToList();
                if (role == "Methodist")
                    groups = (await GetMethodistGroups(userId)).ToList();
            }

            foreach (Enrollment en in await Db.Enrollments.FindAsync(en => en.UserID == userId && en.State == UserGroupState.Applied))
            {
                if (en.State != UserGroupState.Requested && en.State != UserGroupState.Aborted)
                    groups.Add(await Db.Groups.GetAsync(en.EntityID));
            }

            return groups;
        }

        public async Task<IEnumerable<Student>> GetStudents(int? groupId, UserGroupState? state = null)
        {
            if (groupId == null)
                return new List<Student>();
            List<Student> students = new();

            switch (state)
            {
                case UserGroupState.Applied:
                    foreach (Enrollment en in await Db.Enrollments.FindAsync(en => en.EntityID == groupId 
                            && en.State == state && en.Role == UserRole.Student))
                    {
                        students.Add((await Db.Students.FindAsync(st => st.UserId == en.UserID)).SingleOrDefault());
                    }
                    return students;
                case UserGroupState.Requested:
                    int courseId = (await Db.Groups.FindAsync(gr => gr.Id == groupId)).SingleOrDefault()?.CourseId ?? 0;
                    
                    foreach (Enrollment en in await Db.Enrollments.FindAsync(en => en.EntityID == courseId
                    && en.State == state && en.Role == UserRole.Student))
                    {
                        students.Add((await Db.Students.FindAsync(st => st.UserId == en.UserID)).SingleOrDefault());
                    }
                    return students;
                default:
                    return (await GetStudents(groupId, UserGroupState.Applied)).Concat(await GetStudents(groupId, UserGroupState.Requested));
            }
        }

        public async Task<IEnumerable<Teacher>> GetTeachers(int? groupId, UserGroupState? state = null)
        {
            if (groupId == null)
                return new List<Teacher>();
            List<Teacher> teachers = new();
            switch (state)
            {
                case UserGroupState.Applied:
                    foreach (Enrollment en in (await Db.Enrollments.FindAsync(en => en.EntityID == groupId && en.State == state && en.Role == UserRole.Teacher)))
                    {
                        teachers.Add((await Db.Teachers.FindAsync(tc => tc.UserId == en.UserID)).SingleOrDefault());
                    }
                    return teachers;
                case UserGroupState.Requested:
                    int courseId = (await Db.Groups.FindAsync(gr => gr.Id == groupId)).SingleOrDefault()?.CourseId ?? 0;
                    foreach (Enrollment en in await Db.Enrollments.FindAsync(en => en.EntityID == courseId
                        && en.State == state && en.Role == UserRole.Teacher))
                    {
                        teachers.Add((await Db.Teachers.FindAsync(tc => tc.UserId == en.UserID)).SingleOrDefault());
                    }
                    return teachers;
                default:
                    return (await GetTeachers(groupId, UserGroupState.Applied)).Concat(await GetTeachers(groupId, UserGroupState.Requested));
            }
        }

        public async Task<bool> HasStudent(int groupId, string userId)
        {
            return (await Db.Enrollments.FindAsync(en => en.UserID == userId
            && en.Role == UserRole.Student
            && en.EntityID == groupId
            && en.State == UserGroupState.Applied))
                .FirstOrDefault() != null
                ? true : false;
        }

        public async Task<int> CreateAsync(Group entity)
        {
            await Db.Groups.CreateAsync(entity);
            await Db.Save();
            return entity.Id;
        }

        public Task<Group> GetAsync(int id)
        {
            return Db.Groups.GetAsync(id);
        }

        public Task<Group> GetByIdAsync(int id)
        {
            return Db.Groups.GetAsync(id);
        }

        public Task<IEnumerable<Group>> GetAllAsync()
        {
            return Db.Groups.GetAllAsync();
        }

        public async Task<Group> CancelGroupAsync(int groupId)
        {
            Group group = await GetByIdAsync(groupId);
            Enrollment[] enrollments = Array.Empty<Enrollment>();
            foreach (Enrollment enrollment in await Db.Enrollments.FindAsync(en => en.EntityID == groupId 
                    && en.State == UserGroupState.Applied))
            {
                enrollment.State = UserGroupState.Requested;
                enrollment.EntityID = group.CourseId;
                Db.Enrollments.Update(enrollment);
            }
            group.Status = GroupStatus.Pending;
            group.Teacher = null;
            group.TeacherId = null;
            await _studentMarkService.DeleteGroupMarksAsync(groupId);
            
            await UpdateAsync(group);

            return group;
        }

        public async Task<Group> FinishGroupAsync(int groupId)
        {
            Group group = await GetByIdAsync(groupId);

            foreach (Enrollment enrollment in await Db.Enrollments.FindAsync(en => en.EntityID == groupId 
            && en.State == UserGroupState.Applied && en.Role == UserRole.Student))
            {
                await _studentMarkService.SetFinalMarkToStudentForCourse(enrollment.UserID, enrollment.EntityID);
                await Db.Enrollments.DeleteAsync(enrollment.Id);
            }
                
            group.Status = GroupStatus.Pending;


            await UpdateAsync(group);

            return group;
        }

        public async Task<List<SelectListItem>> GetAvailableStatusAsync(int groupId)
        {
            Group group = (await Db.Groups.FindAsync(gr => gr.Id == groupId)).SingleOrDefault();

            List<SelectListItem> statusList = new() { new SelectListItem(group.Status.ToString(), group.Status.ToString()) };
            if (group.Status == GroupStatus.Pending)
                statusList.Add(new SelectListItem(GroupStatus.Started.ToString(), GroupStatus.Started.ToString()));
            if(group.Status == GroupStatus.Started)
            {
                GroupLesson lastLesson = (await Db.GroupLessons.FindAsync(gl => gl.GroupId == groupId)).OrderBy(gl => gl.StartDate).LastOrDefault();
                if (lastLesson.StartDate.Value.AddMinutes(lastLesson.Lesson.Duration) < DateTime.Now)
                    statusList.Add(new SelectListItem(GroupStatus.Finished.ToString(), GroupStatus.Finished.ToString()));
                else statusList.Add(new SelectListItem(GroupStatus.Cancelled.ToString(), GroupStatus.Cancelled.ToString()));
            }
            return statusList;
        }

        public async Task<Methodist> GetCurrentMethodist(int groupId)
        {
            return (await Db.Groups.FindAsync(gr => gr.Id == groupId)).SingleOrDefault()?.Methodist;
        }

        public async Task<IEnumerable<Group>> GetMethodistGroups(string userId)
        {
            Methodist methodist = (await Db.Methodists.FindAsync(m => m.UserId == userId)).SingleOrDefault();
            return await Db.Groups.FindAsync(gr => gr.MethodistId == methodist.Id);
        }
        public async Task<Stream> GetCsvContent(IEnumerable<Group> groups)
        {
            StringBuilder sb = new ();
            sb.AppendLine("Number;Course;MethodistLink;Status;TeacherLink;NumberOfStudents");

            foreach (var group in groups)
            {
                int numberOfStudents = 
                    (await Db.Enrollments.FindAsync(en => en.EntityID == group.Id
                        && en.State == UserGroupState.Applied && en.Role == UserRole.Student))
                            .Count();

                sb.AppendLine($"{group.Number};{group.Course.Title};" +
                    $"{group.Methodist?.LinkToContact};{group.Status};" +
                        $"{group.Teacher?.LinkToProfile};{numberOfStudents}");
            }

            return GenerateStreamFromString(sb.ToString());
        }
        private static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
