using System;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
using System.Threading.Tasks;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ApplicationContext _context;
        private IRepository<Student> _studentRepository;
        private IRepository<Group> _groupRepository;
        private IRepository<Teacher> _teacherRepository;
        private IRepository<Enrollment> _enrollmentsRepository;
        private IRepository<Course> _coursesRepository;
        private IRepository<Topic> _topicsRepository;
        private IRepository<Lesson> _lessonsRepository;
        private IRepository<File> _filesRepository;
        private IRepository<GroupLesson> _groupLessonsRepository;
        private IRepository<StudentMark> _studentMarksRepository;
        private IRepository<Manager> _managersRepository;
        public EFUnitOfWork(ApplicationContext context,
            IRepository<Student> studentRepository,
            IRepository<Group> groupRepository,
            IRepository<Teacher> teacherRepository,
            IRepository<Enrollment> enrollmentsRepository,
            IRepository<Course> coursesRepository,
            IRepository<Topic> topicsRepository,
            IRepository<Lesson> lessonsRepository,
            IRepository<File> filesRepository,
            IRepository<GroupLesson> groupLessonsRepository,
            IRepository<Manager> managersRepository,
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
            _managersRepository = managersRepository;

        }
        public IRepository<Student> Students => _studentRepository;
        public IRepository<Group> Groups => _groupRepository;
        public IRepository<Teacher> Teachers => _teacherRepository;
        public IRepository<Enrollment> Enrollments => _enrollmentsRepository;
        public IRepository<Course> Courses => _coursesRepository;
        public IRepository<Topic> Topics => _topicsRepository;
        public IRepository<Lesson> Lessons => _lessonsRepository;
        public IRepository<File> Files => _filesRepository;
        public IRepository<GroupLesson> GroupLessons => _groupLessonsRepository;
        public IRepository<StudentMark> StudentMarks => _studentMarksRepository;
        public IRepository<Manager> Managers => _managersRepository;

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
