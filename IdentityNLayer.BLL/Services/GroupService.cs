using System;
using System.Collections.Generic;
using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;

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

        public void Create(Group entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Group entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
