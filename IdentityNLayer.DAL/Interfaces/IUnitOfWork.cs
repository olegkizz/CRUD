using System;
using IdentityNLayer.Core.Entities;

namespace IdentityNLayer.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Group> Groups { get; }
        IRepository<Student> Students { get; }
        IRepository<Teacher> Teachers { get; }
        IRepository<StudentToGroupAction> StudentToGroupActions { get; }
        IRepository<Enrollment> Enrollments { get; }
        IRepository<Course> Courses { get; }
        IRepository<Lesson> Lessons { get; }
        IRepository<File> Files { get; }
        IRepository<GroupLesson> GroupLessons { get; }
        IRepository<Topic> Topics { get; }

        void Save();
    }
}
