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

namespace IdentityNLayer.BLL.Services
{
    public class GroupService : IGroupService
    {
        private IUnitOfWork Db { get; set; }
        private IStudentMarkService _studentMarkService{ get; set; }
        public GroupService(IUnitOfWork db,
            IStudentMarkService studentMarkService)
        {
            Db = db;
            _studentMarkService = studentMarkService;
        }

        public void UpdateAsync(Group entity)
        {
            Db.Groups.UpdateAsync(entity);
            Db.Save();
        }

        public Task<EntityEntry<Group>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Teacher GetCurrentTeacher(int groupId)
        {
            return Db.Groups.Find(gr => gr.Id == groupId).FirstOrDefault()?.Teacher;
        }

        public async Task<IEnumerable<Group>> GetGroupsByUserIdAsync(string userId)
        {
            List<Group> groups = new();
            foreach (Enrollment en in Db.Enrollments.Find(en => en.UserID == userId && en.State == UserGroupStates.Applied))
            {
                if (en.State != UserGroupStates.Requested && en.State != UserGroupStates.Aborted)
                    groups.Add(await Db.Groups.GetAsync(en.EntityID));
            }
            return groups;
        }

        public IEnumerable<Student> GetStudents(int? groupId, UserGroupStates? state = null)
        {
            if (groupId == null)
                return new List<Student>();
            List<Student> students = new();
            switch (state)
            {
                case UserGroupStates.Applied:
                    foreach (Enrollment en in Db.Enrollments.Find(en => en.EntityID == groupId && en.State == state && en.Role == UserRoles.Student))
                    {
                        students.Add(Db.Students.Find(st => st.UserId == en.UserID).SingleOrDefault());
                    }
                    return students;
                case UserGroupStates.Requested:
                    foreach (Enrollment en in Db.Enrollments.Find(en => en.EntityID == Db.Groups.Find(gr => gr.Id == groupId).FirstOrDefault()?.CourseId
                    && en.State == state && en.Role == UserRoles.Student))
                    {
                        students.Add(Db.Students.Find(st => st.UserId == en.UserID).SingleOrDefault());
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
                        teachers.Add(Db.Teachers.Find(tc => tc.UserId == en.UserID).SingleOrDefault());
                    }
                    return teachers;
                case UserGroupStates.Requested:
                    foreach (Enrollment en in Db.Enrollments.Find(en => en.EntityID == Db.Groups.Find(gr => gr.Id == groupId).FirstOrDefault()?.CourseId
                    && en.State == state && en.Role == UserRoles.Teacher))
                    {
                        teachers.Add(Db.Teachers.Find(tc => tc.UserId == en.UserID).SingleOrDefault());
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

        public Task<int> CreateAsync(Group entity)
        {
            Db.Groups.CreateAsync(entity);
            Db.Save();
            return Task.FromResult(entity.Id);
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
            foreach (Enrollment enrollment in Db.Enrollments.Find(en => en.EntityID == groupId && en.State == UserGroupStates.Applied))
            {
                enrollment.State = UserGroupStates.Requested;
                enrollment.EntityID = group.CourseId;
                Db.Enrollments.UpdateAsync(enrollment);
            }
            group.Status = GroupStatus.Pending;
            group.Teacher = null;
            group.TeacherId = null;

            _studentMarkService.DeleteGroupMarksAsync(groupId);

            return group;
            //TO DO
            //Delete All Student Marks In The Group
        }

        public async Task<Group> FinishGroupAsync(int groupId)
        {
            Group group = await GetByIdAsync(groupId);

            foreach (Enrollment enrollment in Db.Enrollments.Find(en => en.EntityID == groupId 
            && en.State == UserGroupStates.Applied && en.Role == UserRoles.Student))
                await Db.Enrollments.DeleteAsync(enrollment.Id);
            group.Status = GroupStatus.Pending;
            //UpdateAsync(group);

            return group;
        }

        public Task<List<SelectListItem>> GetAvailableStatusAsync(int groupId)
        {
            Group group = Db.Groups.Find(gr => gr.Id == groupId).SingleOrDefault();

            List<SelectListItem> statusList = new() { new SelectListItem(group.Status.ToString(), group.Status.ToString()) };
            if (group.Status == GroupStatus.Pending)
                statusList.Add(new SelectListItem(GroupStatus.Started.ToString(), GroupStatus.Started.ToString()));
            if(group.Status == GroupStatus.Started)
            {
                GroupLesson lastLesson = Db.GroupLessons.Find(gl => gl.GroupId == groupId).OrderBy(gl => gl.StartDate).LastOrDefault();
                if (lastLesson.StartDate.Value.AddMinutes(lastLesson.Lesson.Duration) < DateTime.Now)
                    statusList.Add(new SelectListItem(GroupStatus.Finished.ToString(), GroupStatus.Finished.ToString()));
                else statusList.Add(new SelectListItem(GroupStatus.Cancelled.ToString(), GroupStatus.Cancelled.ToString()));
            }
            return Task.FromResult(statusList);
        }
    }
}
