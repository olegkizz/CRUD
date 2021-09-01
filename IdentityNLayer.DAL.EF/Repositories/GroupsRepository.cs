﻿using System;
using System.Collections.Generic;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            return _context.Groups
                .Include(g => g.Enrollments)
                .Where(g => g.Id == id)
                .First();
        }

        public IEnumerable<Group> Find(Func<Group, bool> predicate)
        {
            return _context.Groups.Where(predicate).ToList();
        }

        public void Create(Group item)
        {
            _context.Groups.Add(item);
        }

        public void Update(Group item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.Groups.Update(item);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
