using System;
using System.Collections.Generic;
using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
using System.Linq;

namespace IdentityNLayer.BLL.Services
{
    public class GroupService : IGroupService
    {
        private IUnitOfWork Db { get; set; }
        public GroupService(IUnitOfWork db)
        {
            Db = db;
        }
        public IEnumerable<Group> GetAll()
        {
            return Db.Groups.GetAll();
        }
        public Group GetById(int id)
        {
            return Db.Groups.Get(id);
        }

        public int Create(Group entity)
        {
            Db.Groups.Create(entity);
            Db.Save();
            return entity.Id;
        }

        public void Update(Group entity)
        {
            Db.Groups.Update(entity);
            Db.Save();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Teacher GetTeacher(int groupId)
        {
            return Db.Groups.Find(gr => gr.Id == groupId ).FirstOrDefault()?.Teacher;
        }
    }
}
