using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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
        public DbSet<Methodist> Methodists { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(
                @"Server=nb_oleg\mssqlserver2;Database=WEBApp;
                    Trusted_Connection=True;"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                   .Entity<Course>()
                   .HasMany(c => c.Lessons)
                   .WithOne(l => l.Course)
                   .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Course>()
            .HasOne(v => v.Topic)
            .WithMany()
            .HasForeignKey(v => v.TopicId)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Group>()
             .HasOne(g => g.Methodist)
             .WithMany()
             .HasForeignKey(v => v.MethodistId)
             .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Seed();
        }
    }
}