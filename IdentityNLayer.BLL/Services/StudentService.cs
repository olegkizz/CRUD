using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

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
        public int Create(Student student)
        {
            Db.Students.Create(student);
            Db.Save();
            return student.Id;
        }
 /*       public async Task<int> CreateAsync(Student student)
        {
            IdentityRole identityRole = new IdentityRole();
            identityRole.Name = UserRoles.Student.ToString();
            await _roleManager.CreateAsync(identityRole);

            IdentityResult chkUser = await _userManager.CreateAsync(student.User, student.User.PasswordHash);

            //Add default User to Role Admin    
            if (chkUser.Succeeded)
            {
                await _userManager.AddToRoleAsync(student.User, identityRole.Name);
            }
            return Create(student);
        }*/
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
            foreach (Enrollment en in Db.Enrollments.Find(en => en.UserID == Db.Students.Get(studentId).UserId))
            {
                if(en.State != ActionsStudentGroup.Aborted)
                    groups.Add(Db.Groups.Get(en.EntityID));
            }
            return groups;
        }
    }
}
