using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Services
{
    public class CourseService : ICourseService
    {
        private IUnitOfWork Db { get; set; }

        public CourseService(IUnitOfWork db)
        {
            Db = db;
        }
        public int Create(Course entity)
        {
            Db.Courses.Create(entity);
            Db.Save();
            return entity.Id;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Course> GetAll()
        {
            return Db.Courses.GetAll();
        }

        public Course GetById(int id)
        {
            return Db.Courses.Get(id);
        }
        
        public void Update(Course entity)
        {
            Db.Courses.Update(entity);
            Db.Save();
        }

        public IEnumerable<Enrollment> GetStudentRequests(int id)
        {
            return Db.Enrollments.Find(en => en.EntityID == id && en.State == ActionsStudentGroup.Requested && en.Role == UserRoles.Student);
        }
        public IEnumerable<Enrollment> GetTeacherRequests(int id)
        {
            return Db.Enrollments.Find(en => en.EntityID == id && en.State == ActionsStudentGroup.Requested && en.Role == UserRoles.Teacher);
        }
        public IEnumerable<Topic> GetAvailableTopics()
        {
            return Db.Topics.GetAll();
        }
    }
}
