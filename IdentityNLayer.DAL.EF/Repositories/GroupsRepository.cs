﻿using System;
using System.Collections.Generic;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class GroupsRepository : IRepository<Group>
    {
        private ApplicationContext _context;

        public GroupsRepository(ApplicationContext context)
        {
            _context = context;
        }
        public IEnumerable<Group> Find(Func<Group, bool> predicate)
        {
            return _context.Groups
                .Include(gr => gr.Teacher)
                .Include(gr => gr.Enrollments)
                .AsNoTracking()
                .Where(predicate).ToList();
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

        public async Task<IEnumerable<Group>> FindAsync(Func<Group, bool> predicate)
        {
            return await _context.Groups
              .Include(gr => gr.Teacher)
              .Include(gr => gr.Enrollments)
              .AsNoTracking()
              .Where(predicate)
              .AsQueryable()
              .ToListAsync();
        }

        public async void CreateAsync(Group item)
        {
            await _context.Groups.AddAsync(item);

        }

        public Task<Group> GetAsync(int id)
        {
            return _context.Groups
               .Include(g => g.Enrollments)
               .Include(g => g.Teacher)
               .AsNoTracking()
               .Where(g => g.Id == id)
               .SingleAsync();
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await _context.Groups
                .Include(g => g.Enrollments)
                .Include(g => g.Teacher)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
