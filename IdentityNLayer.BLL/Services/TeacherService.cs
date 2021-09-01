﻿using System;
using System.Collections.Generic;
using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
using System.Linq;

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

        public int Create(Teacher entity)
        {
            Db.Teachers.Create(entity);
            Db.Save();
            return entity.Id;
        }

        public void Update(Teacher entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Teacher GetTeacherByUserId(string userId)
        {
            foreach(Teacher tc in Db.Teachers.Find(tc => tc.UserId == userId))
                return tc;
            return null;
        }
        public bool HasAccount(string userId)
        {
            return Db.Teachers.Find(tc => tc.UserId == userId).Any();
        }
    }
}
