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
            Db.Students.Create(student);
            Db.Save();
        }
   /*     public void Create(Student student, int[] selectedGroups = null)
        {
            *//*.ForMember("Group", opt
                => opt.MapFrom(st => Db.Students.Get(st.GroupId)))*//*
            foreach(int groupId in selectedGroups)
            {
                Db.Enrollments.Create(new Enrollment()
                {

                });
            }
            Create(student);
            Db.Save();
        }*/

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
        public List<Group> GetStudentGroups(int studentId)
        {
            List<Group> groups = new();
            foreach (Enrollment en in Db.Students.Get(studentId).Enrollments)
            {
                groups.Add(en.Group);
            }
            return groups;
        }

        public void Enrol(int studentId, int groupdId, bool confirmed = true)
        {
            int enrollmentExists =
                ((List<Enrollment>)Db.Enrollments.Find(en => en.StudentID == studentId && en.GroupID == groupdId)).Count;
            if(enrollmentExists == 0)
                Db.Enrollments.Create(new Enrollment
                {
                    StudentID = studentId,
                    GroupID = groupdId,
                    State = confirmed ? ActionsStudentGroup.Applied : ActionsStudentGroup.Requested
                });
        }
        public void UnEnrol(int studentId, int groupdId, bool confirmed = true)
        {
            Enrollment enrollment = 
                ((List<Enrollment>)Db.Enrollments.Find(en => en.StudentID == studentId && en.GroupID == groupdId))[0];
            if (enrollment != null)
            {
                enrollment.State = ActionsStudentGroup.Aborted;
                Db.Enrollments.Update(enrollment);
            } else throw new ArgumentNullException($"Student {studentId} is not enrol in th group {groupdId}");
        }
    }
}
