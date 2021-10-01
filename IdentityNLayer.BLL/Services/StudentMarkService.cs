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
        private IStudentService _studentService { get; set; }

        public StudentMarkService(IUnitOfWork db,
            IGroupLessonService groupLessonService,
            IStudentService studentService)
        {
            Db = db;
            _groupLessonService = groupLessonService;
            _studentService = studentService;
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
                gl => gl.StartDate.Value.AddMinutes(gl.Lesson.Duration) < DateTime.Now).ToList())
            {
                StudentMark studentMark = (await Db.StudentMarks.FindAsync(sm => sm.LessonId == lesson.LessonId
                 && sm.StudentId == studentId)).SingleOrDefault();
                if (studentMark == null)
                    studentMark = await GetByIdAsync(await CreateAsync(new StudentMark
                    {
                        StudentId = studentId,
                        LessonId = lesson.LessonId,
                        Mark = null
                    }));
                studentMarks.Add(studentMark);
            }
            return studentMarks;
        }

        public async Task<StudentMark> GetByStudentAndLessonIdAsync(int studentId, int lessonId)
        {
            return (await Db.StudentMarks.FindAsync(sm => sm.StudentId == studentId && sm.LessonId == lessonId)).SingleOrDefault();
        }

        public async Task DeleteGroupMarksAsync(int groupId)
        {
            List<Student> students = new();
            List<StudentMark> studentMarks = new();

            foreach (Enrollment en in await Db.Enrollments.FindAsync(en => en.EntityID == groupId 
                && en.State == UserGroupStates.Applied && en.Role == UserRoles.Student))
            {
                students.Add((await Db.Students.FindAsync(st => st.UserId == en.UserID)).SingleOrDefault());
            }
            foreach (Student student in students)
            {
                foreach (GroupLesson groupLesson in await _groupLessonService.GetLessonsByGroupIdAsync(groupId))
                {
                    StudentMark studentMark = await GetByStudentAndLessonIdAsync(student.Id, groupLesson.LessonId);
                    if (studentMark != null)
                        await Delete(studentMark.Id);
                }
            }
        }

        public async Task<int?> GetMarkByStudentAndGroupIdAsync(int studentId, int courseId)
        {
            return (await Db.StudentMarks.FindAsync(sm => sm.StudentId == studentId && sm.CourseId == courseId))
                .Select(StudentMark => StudentMark.Mark).SingleOrDefault();
        }

        public async Task<int> SetFinalMarkToStudentForCourse(string userId, int courseId)
        {
            Student student = (await Db.Students.FindAsync(s => s.UserId == userId)).SingleOrDefault();
            int groupId = (await _studentService.GetGroupByCourseIdAsync(student.Id, courseId)).Id;
            int i = 0;
            int mark = 0;
            foreach (GroupLesson groupLesson in (await Db.GroupLessons.FindAsync
                (gl => gl.GroupId == groupId)).ToList())
            {
                i++;
                mark += (await Db.StudentMarks.FindAsync(sm => sm.StudentId == student.Id
                        && sm.LessonId == groupLesson.LessonId))?.Select(sm => sm.Mark)?.SingleOrDefault() ?? 0;
            }
            await CreateAsync(new StudentMark
            {
                Mark = mark / (i == 0 ? 1 : i),
                StudentId = student.Id,
                CourseId = courseId
            });
            return mark;
        }

        public async Task<IEnumerable<StudentMark>> GetByLessonIdAsync(int lessonId)
        {
            return await Db.StudentMarks.FindAsync(sm => sm.LessonId == lessonId);
        }

        public async Task<IEnumerable<StudentMark>> GetMarksOfCoursesAsync(int studentId)
        {
            return await Db.StudentMarks.FindAsync(sm => sm.LessonId == null && sm.StudentId == studentId);
        }
    }
}
