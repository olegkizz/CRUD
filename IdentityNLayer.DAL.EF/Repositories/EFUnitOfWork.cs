using System;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ApplicationContext _context;
        private IRepository<Student> _studentRepository;
        private IRepository<Group> _groupRepository;
        private IRepository<Teacher> _teacherRepository;
        private IRepository<StudentToGroupAction> _studentToGroupActionsRepository;
        private IRepository<Enrollment> _enrollmentsRepository;
        private IRepository<Course> _coursesRepository;
        private IRepository<Topic> _topicsRepository;

        public EFUnitOfWork(ApplicationContext context,
            IRepository<Student> studentRepository,
            IRepository<Group> groupRepository,
            IRepository<Teacher> teacherRepository,
            IRepository<StudentToGroupAction> studentToGroupActionsRepository,
            IRepository<Enrollment> enrollmentsRepository,
            IRepository<Course> coursesRepository,
            IRepository<Topic> topicsRepository)
        {
            _context = context;
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
            _teacherRepository = teacherRepository;
            _studentToGroupActionsRepository = studentToGroupActionsRepository;
            _enrollmentsRepository = enrollmentsRepository;
            _coursesRepository = coursesRepository;
            _topicsRepository = topicsRepository;
        }
        public IRepository<Student> Students => _studentRepository;
        public IRepository<Group> Groups => _groupRepository;
        public IRepository<Teacher> Teachers => _teacherRepository;
        public IRepository<StudentToGroupAction> StudentToGroupActions => _studentToGroupActionsRepository;
        public IRepository<Enrollment> Enrollments => _enrollmentsRepository;
        public IRepository<Course> Courses => _coursesRepository;
        public IRepository<Topic> Topics => _topicsRepository;

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
