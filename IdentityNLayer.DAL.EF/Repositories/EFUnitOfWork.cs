using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.DAL.Entities;
using IdentityNLayer.DAL.Interfaces;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private StudentContext _context;
        private IRepository<Student> _studentRepository;
        private IRepository<Group> _groupRepository;

        public EFUnitOfWork(StudentContext context,
            IRepository<Student> studentRepository,
            IRepository<Group> groupRepository)
        {
            _context = context;
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
        }
        public IRepository<Student> Students => _studentRepository;

        public IRepository<Group> Groups => _groupRepository;
        

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
