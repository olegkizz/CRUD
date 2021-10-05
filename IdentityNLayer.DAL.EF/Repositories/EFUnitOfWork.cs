using System;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
using System.Threading.Tasks;
using IdentityNLayer.Core.Filters;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ApplicationContext _context;
        private IRepository<Student> _studentRepository;
        private IRepository<Group> _groupRepository;
        private IRepository<Teacher> _teacherRepository;
        private IRepository<Enrollment> _enrollmentsRepository;
        private IFilterRepository<Course, CourseFilter> _coursesRepository;
        private IRepository<Topic> _topicsRepository;
        private IRepository<Lesson> _lessonsRepository;
        private IRepository<File> _filesRepository;
        private IRepository<GroupLesson> _groupLessonsRepository;
        private IRepository<StudentMark> _studentMarksRepository;
        private IRepository<Methodist> _methodistsRepository;
        public EFUnitOfWork(ApplicationContext context,
            IRepository<Student> studentRepository,
            IRepository<Group> groupRepository,
            IRepository<Teacher> teacherRepository,
            IRepository<Enrollment> enrollmentsRepository,
            IFilterRepository<Course, CourseFilter> coursesRepository,
            IRepository<Topic> topicsRepository,
            IRepository<Lesson> lessonsRepository,
            IRepository<File> filesRepository,
            IRepository<GroupLesson> groupLessonsRepository,
            IRepository<Methodist> methodistsRepository,
            IRepository<StudentMark> studentMarksRepository)
        {
            _context = context;
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
            _teacherRepository = teacherRepository;
            _enrollmentsRepository = enrollmentsRepository;
            _coursesRepository = coursesRepository;
            _topicsRepository = topicsRepository;
            _lessonsRepository = lessonsRepository;
            _filesRepository = filesRepository;
            _groupLessonsRepository = groupLessonsRepository;
            _studentMarksRepository = studentMarksRepository;
            _methodistsRepository = methodistsRepository;

        }
        public IRepository<Student> Students => _studentRepository;
        public IRepository<Group> Groups => _groupRepository;
        public IRepository<Teacher> Teachers => _teacherRepository;
        public IRepository<Enrollment> Enrollments => _enrollmentsRepository;
        public IFilterRepository<Course, CourseFilter> Courses => _coursesRepository;
        public IRepository<Topic> Topics => _topicsRepository;
        public IRepository<Lesson> Lessons => _lessonsRepository;
        public IRepository<File> Files => _filesRepository;
        public IRepository<GroupLesson> GroupLessons => _groupLessonsRepository;
        public IRepository<StudentMark> StudentMarks => _studentMarksRepository;
        public IRepository<Methodist> Methodists => _methodistsRepository;

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.DisposeAsync();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
