using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Services
{
    public class StudentMarkService : IStudentMarkService
    {
        private IUnitOfWork Db { get; set; }
        private IGroupLessonService _groupLessonService { get; set; }

        public StudentMarkService(IUnitOfWork db,
            IGroupLessonService groupLessonService)
        {
            Db = db;
            _groupLessonService = groupLessonService;
        }
        public Task<int> CreateAsync(StudentMark entity)
        {
            Db.StudentMarks.CreateAsync(entity);
            Db.Save();
            return Task.FromResult(entity.Id);
        }

        public async Task<EntityEntry<StudentMark>> Delete(int id)
        {
            EntityEntry<StudentMark> entry = await Db.StudentMarks.DeleteAsync(id);
            Db.Save();
            return entry;
        }

        public Task<IEnumerable<StudentMark>> GetAllAsync()
        {
            return Db.StudentMarks.GetAllAsync();
        }

        public Task<StudentMark> GetByIdAsync(int id)
        {
            return Db.StudentMarks.GetAsync(id);
        }

        public void UpdateAsync(StudentMark entity)
        {
            Db.StudentMarks.UpdateAsync(entity);
            Db.Save();
        }

        public async Task<IEnumerable<StudentMark>> GetMarksByGroupAndStudentIdAsync(int groupId, int studentId)
        {
            List<StudentMark> studentMarks = new();
            foreach (GroupLesson lesson in (await _groupLessonService.GetLessonsByGroupIdAsync(groupId)).Where(
                gl => gl.StartDate.Value.AddMinutes(gl.Lesson.Duration) < DateTime.Now))
            {
                StudentMark studentMark = Db.StudentMarks.Find(sm => sm.LessonId == lesson.Id
                 && sm.StudentId == studentId)?.SingleOrDefault();
                if (studentMark == null)
                    studentMark = await GetByIdAsync(await CreateAsync(new StudentMark
                    {
                        StudentId = studentId,
                        LessonId = lesson.Id,
                        Mark = null
                    }));
                studentMarks.Add(studentMark);
            }
            return studentMarks;
        }

        public Task<StudentMark> GetByStudentAndLessonIdAsync(int studentId, int lessonId)
        {
            return Task.FromResult(Db.StudentMarks.Find(sm => sm.StudentId == studentId && sm.LessonId == lessonId)?.SingleOrDefault());
        }

        public async void DeleteGroupMarksAsync(int groupId)
        {
            List<Student> students = new();

            foreach (Enrollment en in Db.Enrollments.Find(en => en.EntityID == groupId && en.State == UserGroupStates.Applied && en.Role == UserRoles.Student))
            {
                students.Add(Db.Students.Find(st => st.UserId == en.UserID).SingleOrDefault());
            }
            foreach (Student student in students)
            {
                foreach (GroupLesson groupLesson in await _groupLessonService.GetLessonsByGroupIdAsync(groupId))
                    await Delete((await GetByStudentAndLessonIdAsync(student.Id, groupLesson.LessonId)).Id);
            }
        }

        public Task<List<Group>> GetStudentGroupsAsync(int studentId)
        {
            List<Group> groups = new();
            foreach (StudentMark studentMark in Db.StudentMarks.Find(sm => sm.StudentId == studentId))
            {
                Group group = Db.GroupLessons.Find(gl => gl.LessonId == studentMark.LessonId)?.FirstOrDefault()?.Group;
                if (groups.Find(gr => gr.Id == group.Id) == null)
                    groups.Add(group);
            }
            return Task.FromResult(groups);
        }

        public Task<int> GetMarkByStudentAndGroupIdAsync(int studentId, int groupId)
        {
            int mark = 0;
            int i = 0;
            foreach (GroupLesson groupLesson in Db.GroupLessons.Find(gl => gl.GroupId == groupId).ToList())
            {
                i++;
                mark += Db.StudentMarks.Find(sm => sm.StudentId == studentId
                        && sm.LessonId == groupLesson.LessonId)?.Select(sm => sm.Mark)?.SingleOrDefault() ?? 0;
            }
            return Task.FromResult(mark / (i == 0 ? 1 : i));
        }
    }
}
