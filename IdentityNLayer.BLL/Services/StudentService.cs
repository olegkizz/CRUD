using System;
using System.Collections.Generic;
using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;

namespace IdentityNLayer.BLL.Services
{
    public class StudentService : IStudentService
    {
        private IUnitOfWork Db { get; set; }
        public StudentService(IUnitOfWork db)
        {
            Db = db;
        }
        public IEnumerable<Student> GetAll()
        {
            return Db.Students.GetAll();
        }
        public Student GetById(int id)
        {
            return Db.Students.Get(id);
        }
        public void Create(Student student)
        {
            /*.ForMember("Group", opt
                => opt.MapFrom(st => Db.Students.Get(st.GroupId)))*/
            Db.Students.Create(student);
            Db.Save();
        }

        public Array GetStudentTypes()
        {
            return Enum.GetValues(typeof(StudentType));
        }

        public void Update(Student entity)
        {
            Db.Students.Update(entity);
            Db.Save();
        }

        public void Delete(int id)
        {
            Db.Students.Delete(id);
            Db.Save();
        }
    }
}
