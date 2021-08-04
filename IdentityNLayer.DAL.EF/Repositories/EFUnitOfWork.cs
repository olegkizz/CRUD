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

        public EFUnitOfWork(ApplicationContext context,
            IRepository<Student> studentRepository,
            IRepository<Group> groupRepository,
            IRepository<Teacher> teacherRepository)
        {
            _context = context;
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
            _teacherRepository = teacherRepository;
        }
        public IRepository<Student> Students => _studentRepository;
        public IRepository<Group> Groups => _groupRepository;
        public IRepository<Teacher> Teachers => _teacherRepository;


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
