using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;

namespace IdentityNLayer.DAL.EF.Context
{
    public class ApplicationContext : IdentityDbContext<Person>
    {

        public ApplicationContext()
        {

        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<StudentToGroupAction> StudentToGroupActions { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<GroupLesson> GroupLessons { get; set; }
        public DbSet<StudentMark> StudentMarks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = WEBApp; Trusted_Connection = True; MultipleActiveResultSets = true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Course>().HasOne(gl => gl.Topic).WithOne(t => t.Course).HasForeignKey<Topic>(t => t.CourseId);
            modelBuilder
                   .Entity<Course>()
                   .HasMany(c => c.Lessons)
                   .WithOne(l => l.Course)
                   .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Seed();
        }
    }
}