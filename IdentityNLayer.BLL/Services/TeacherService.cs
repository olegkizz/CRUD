using System;
using System.Collections.Generic;
using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;

namespace IdentityNLayer.BLL.Services
{
    public class TeacherService : ITeacherService
    {
        private IUnitOfWork Db { get; set; }
        public TeacherService(IUnitOfWork db)
        {
            Db = db;
        }

        public IEnumerable<Teacher> GetAll()
        {
            return Db.Teachers.GetAll();
        }
        public Teacher GetById(int id)
        { 
            return Db.Teachers.Get(id);
        }

        public void Create(Teacher entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Teacher entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
