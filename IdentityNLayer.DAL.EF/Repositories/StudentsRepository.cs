using System;
using System.Collections.Generic;
using System.Linq;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class StudentsRepository : IRepository<Student>
    {
        private ApplicationContext _context;

        public StudentsRepository(ApplicationContext context)
        {
            _context = context;
        }        
        public IEnumerable<Student> GetAll()
        {
            return _context.Students
                .ToList();
        }

        public Student Get(int id)
        {
            return _context.Students
                .Include(s => s.User)
                .Where(s => s.Id == id)
                .First();
        }

        public IEnumerable<Student> Find(Func<Student, bool> predicate)
        {
            return _context.Students.Where(predicate).ToList();
        }

        public void Create(Student item)
        {
            _context.Students.Add(item);
        }

        public void Update(Student item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.Students.Update(item);
            if (item.User != null)
            {
                IdentityUser user = _context.Users.Single(us => us.Id == item.UserId);
            
                user.Email = item.User.Email;
                user.NormalizedEmail = item.User.Email.ToUpper();
                user.PhoneNumber = item.User.PhoneNumber;
  /*              _context.Entry(item.User).Property("Email").IsModified = true;
                _context.Entry(item.User).Property("NormalizedEmail").IsModified = true;
                _context.Entry(item.User).Property("PhoneNumber").IsModified = true;*/
                /*_context.Attach(user);*/
                /* item.User.NormalizedEmail = item.User.Email.ToUpper();*/
                _context.Users.Update(user);
            }
        }

        public void Delete(int id)
        {
            Student student = _context.Students.Find(id);
            if (student != null)
                _context.Students.Remove(student);
        }

        public IEnumerable<Student> FindAsync(Func<Student, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void CreateAsync(Student item)
        {
            throw new NotImplementedException();
        }
    }
}