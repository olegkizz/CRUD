using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityNLayer.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityNLayer.DAL.EF.Context
{
    public class StudentContext : IdentityDbContext
    {
        public StudentContext()
        {
        }
        public StudentContext(DbContextOptions<StudentContext> options)
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = aspnet - CRUD - 2C2C45A4 - 7668 - 49D7 - 9231 - 35AFE5DBFBAA; Trusted_Connection = True; MultipleActiveResultSets = true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Group firstGroup = new Group
            {
                Id = 1,
                Name = "FirstGroup",
                OwnerId = "420e09c7-4d2a-4f0e-b780-27055dc90111"
            };
            Group secondGroup = new Group
            {
                Id = 2,
                Name = "SecondGroup",
                OwnerId = "8379999c-7a8a-457e-925e-4cde670c251d"
            };
            modelBuilder.Entity<Group>().HasData(
                new Group[]
                {
                    firstGroup, secondGroup
                }
            );
            modelBuilder.Entity <Student>().HasData(
                new Student[]
                {
                    new Student
                    {
                        Id = 1, Name = "Oleg", Address = "First", City = "Moscow", Email = "olegkizz@mail.com",
                        State = "Russia", Zip = "213", GroupId = firstGroup.Id
                    },
                    new Student
                    {
                        Id = 2, Name = "Vova", Address = "Fist", City = "Mocow", Email = "olegkzz@mail.com",
                        State = "Russia", Zip = "23", GroupId = firstGroup.Id
                    },
                    new Student
                    {
                        Id = 3, Name = "Nikita", Address = "First", City = "Moscow", Email = "olegkizz@mail.com",
                        State = "Belarus", Zip = "213", GroupId = secondGroup.Id
                    }
                }
            );
        }
    }
}