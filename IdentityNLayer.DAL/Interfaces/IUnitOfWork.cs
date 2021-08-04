using System;
using IdentityNLayer.Core.Entities;

namespace IdentityNLayer.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Group> Groups { get; }
        IRepository<Student> Students { get; }
        IRepository<Teacher> Teachers { get; }

        void Save();
    }
}
