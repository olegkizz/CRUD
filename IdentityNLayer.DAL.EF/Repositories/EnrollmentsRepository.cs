﻿using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.EF.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class EnrollmentsRepository : IRepository<Enrollment>
    {
        private ApplicationContext _context;

        public EnrollmentsRepository(ApplicationContext context)
        {
            _context = context;
        }
        public IEnumerable<Enrollment> GetAll()
        {
            return _context.Enrollments
               .Include(s => s.User)
               .AsNoTracking()
               .ToList();
        }

        public Enrollment Get(int id)
        {
            return _context.Enrollments
                .Include(s => s.User)
                .AsNoTracking()
                .Where(s => s.Id == id)
                .Single();
        }

        public IEnumerable<Enrollment> Find(Func<Enrollment, bool> predicate)
        {
            return _context.Enrollments
                .Include(en => en.User)
                .AsNoTracking()
                .Where(predicate)
                .ToList();
        }

        public void Create(Enrollment item)
        {
            _context.Enrollments.Add(item);
        }

        public void Update(Enrollment item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.Enrollments.Update(item);
        }

        public void Delete(int id)
        {
            Enrollment enrol = _context.Enrollments.Find(id);
            if (enrol != null)
                _context.Enrollments.Remove(enrol);
        }

        public async Task<IEnumerable<Enrollment>> FindAsync(Func<Enrollment, bool> predicate)
        {
            return await _context.Enrollments
                .Include(en => en.User)
                .Where(predicate)
                .AsQueryable()
                .ToListAsync();
        }

        public void CreateAsync(Enrollment item)
        {
            throw new NotImplementedException();
        }

        public async Task<Enrollment> GetAsync(int id)
        {
            return await _context.Enrollments
             .Include(s => s.User)
             .AsNoTracking()
             .Where(s => s.Id == id)
             .SingleAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            return await _context.Enrollments
               .Include(s => s.User)
               .AsNoTracking()
               .ToListAsync();
        }
    }
}
