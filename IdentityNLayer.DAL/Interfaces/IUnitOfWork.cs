using System;
using System.Threading.Tasks;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.Core.Filters;

namespace IdentityNLayer.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Group> Groups { get; }
        IRepository<Student> Students { get; }
        IRepository<Teacher> Teachers { get; }
        IRepository<Enrollment> Enrollments { get; }
        IFilterRepository<Course, CourseFilter> Courses { get; }
        IRepository<Lesson> Lessons { get; }
        IRepository<File> Files { get; }
        IRepository<GroupLesson> GroupLessons { get; }
        IRepository<Topic> Topics { get; }
        IRepository<StudentMark> StudentMarks { get; }
        IRepository<Methodist> Methodists { get; }

        Task Save();
    }
}
