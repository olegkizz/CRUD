using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<List<Group>> GetStudentGroupsAsync(int studentId)
        {
            List<Group> groups = new();
            
            Student student = await Db.Students.GetAsync(studentId);
            foreach (Enrollment en in Db.Enrollments.Find(en => en.UserID == student.UserId))
            {
                if (en.State != UserGroupStates.Aborted && en.State != UserGroupStates.Requested)
                    groups.Add(await Db.Groups.GetAsync(en.EntityID));
            }
            return groups;
        }

        public bool HasAccount(string userId)
        {
            return Db.Students.Find(st => st.UserId == userId).Any();
        }

        public Student GetByUserId(string userId)
        {
            return Db.Students.Find(st => st.UserId == userId).SingleOrDefault();
        }

        public async Task<Group> GetGroupByCourseIdAsync(int studentId, int courseId)
        {
            foreach (Group gr in await GetStudentGroupsAsync(studentId))
                if (gr.CourseId == courseId)
                    return gr;
            return null;
        }

        public Task<int> CreateAsync(Student entity)
        {
            Db.Students.CreateAsync(entity);
            Db.Save();
            return Task.FromResult(entity.Id);
        }

        public Task<Student> GetByIdAsync(int id)
        {
            return Db.Students.GetAsync(id);
        }

        public Task<IEnumerable<Student>> GetAllAsync()
        {
            return Db.Students.GetAllAsync();
        }
    }
}
