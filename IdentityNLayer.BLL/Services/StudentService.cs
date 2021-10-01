﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace IdentityNLayer.BLL.Services
{
    public class StudentService : IStudentService
    {
        private IUnitOfWork Db { get; set; }

        public StudentService(IUnitOfWork db)
        {
            Db = db;
           
        }

        public Array GetStudentTypes()
        {
            return Enum.GetValues(typeof(StudentType));
        }

        public void UpdateAsync(Student entity)
        {
            Db.Students.UpdateAsync(entity);
            Db.Save();
        }

        public async Task<EntityEntry<Student>> Delete(int id)
        {
            EntityEntry<Student> entity = await Db.Students.DeleteAsync(id);
            Db.Save();
            return entity;

        }
        public async Task<List<Group>> GetStudentGroupsAsync(int studentId)
        {
            List<Group> groups = new();
            
            Student student = await Db.Students.GetAsync(studentId);
            foreach (Enrollment en in await Db.Enrollments.FindAsync(en => en.UserID == student.UserId))
            {
                if (en.State != UserGroupStates.Aborted && en.State != UserGroupStates.Requested)
                    groups.Add(await Db.Groups.GetAsync(en.EntityID));
            }
            return groups;
        }

        public async Task<bool> HasAccount(string userId)
        {
            return (await Db.Students.FindAsync(st => st.UserId == userId)).Any();
        }

        public async Task<Student> GetByUserId(string userId)
        {
            return (await Db.Students.FindAsync(st => st.UserId == userId)).SingleOrDefault();
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
