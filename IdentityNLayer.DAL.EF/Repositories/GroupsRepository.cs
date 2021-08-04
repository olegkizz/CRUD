﻿using System;
using System.Collections.Generic;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.Core.Entities;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class GroupsRepository : IRepository<Group>
    {
        private ApplicationContext _context;

        public GroupsRepository(ApplicationContext context)
        {
            _context = context;
        }
        public IEnumerable<Group> GetAll()
        {
            return _context.Groups;
        }

        public Group Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> Find(Func<Group, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Create(Group item)
        {
            throw new NotImplementedException();
        }

        public void Update(Group item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
