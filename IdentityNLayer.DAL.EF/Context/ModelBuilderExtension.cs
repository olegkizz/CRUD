using System;
using System.Collections.Generic;
using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityNLayer.DAL.EF.Context
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            /* var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
             IdentityUser user = new IdentityUser
             {
                 UserName = "AdminTest",
                 EmailConfirmed = true,
                 Email = "AdminTest@admin.com"
             };
             await userManager.CreateAsync(user, "Kiselev12-");

             string role = "Admin";
             var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
             IdentityResult IR = await roleManager.CreateAsync(new IdentityRole(role));
             IR = await userManager.AddToRoleAsync(user, role);
 */
            Teacher teacher1 = new Teacher
            {
                Id = 1,
                FirstName = "Teach",
                LastName = "First",
                Email = "Teach1@teacher.com",
                BirthDate = new DateTime(1985, 2, 21),
                Bio = "Super Teacher"
            };
            Teacher teacher2 = new Teacher
            {
                Id = 2,
                FirstName = "Teach",
                LastName = "Second",
                Email = "Teach2@teacher.com",
                BirthDate = new DateTime(1992, 2, 21),
                Bio = "Super Teacher"
            };

            modelBuilder.Entity<Teacher>().HasData(
               new Teacher[]
               {
                   teacher1, teacher2
               }
           );
            Topic topic1 = new Topic()
            {
                Id = 1,
                Title = ".NET",
                Description = "Super MVC"
            };
            Topic topic2 = new Topic()
            {
                Id = 2,
                Title = "Spring",
                Description = "Super Spring"
            };
            modelBuilder.Entity<Topic>().HasData(
             new Topic[]
             {
                    topic1, topic2
             });
            Course course1 = new Course()
            {
                Id = 1,
                Created = DateTime.Now,
                Title = "ASP",
                Description = "Super MVC",
                TopicId = topic1.Id
            };
            Course course2 = new Course()
            {
                Id = 2,
                Created = DateTime.Now,
                Title = "Java",
                Description = "Super Spring",
                TopicId = topic2.Id
            };
            modelBuilder.Entity<Course>().HasData(
                new Course[]
                {
                    course1, course2
                });
            Group firstGroup = new Group
            {
                Id = 1,
                Number = "Nemiga-1",
                Status = GroupStatus.Started,
                TeacherId = teacher1.Id,
                StartDate = new DateTime(2021, 9, 21),
                CourseId = course1.Id
            };
            Group secondGroup = new Group
            {
                Id = 2,
                Number = "Nemiga-2",
                Status = GroupStatus.Pending,
                TeacherId = teacher2.Id,
                StartDate = new DateTime(2021, 10, 21),
                CourseId = course2.Id
            };
            modelBuilder.Entity<Group>().HasData(
                new Group[]
                {
                    firstGroup, secondGroup
                }
            );
            Student studentFirst = new Student
            {
                Id = 1,
                FirstName = "Oleg",
                LastName = "Kizz",
                Type = StudentType.Online
            };
            Student studentSecond = new Student
            {
                Id = 2,
                FirstName = "Vova",
                LastName = "Braslav",
                Type = StudentType.Offline
            };
            Student studentThird = new Student
            {
                Id = 3,
                FirstName = "Nikita",
                LastName = "Chebur",
                Type = StudentType.Mix
            };
            modelBuilder.Entity<Student>().HasData(
             new Student[]
             {
                    studentFirst, studentSecond, studentThird
             }
         );
            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment[]
                {
                    new Enrollment {
                        Id = 1,
                        StudentID = studentFirst.Id,
                        GroupID = firstGroup.Id,
                        Created = new DateTime(2021, 6, 15),
                     
                    },
                     new Enrollment {
                        Id = 2,
                        StudentID = studentFirst.Id,
                        GroupID = secondGroup.Id,
                        Created = new DateTime(2021, 7, 15),
                     },
                    new Enrollment {
                        Id = 3,
                        StudentID = studentSecond.Id,
                        GroupID = secondGroup.Id,
                        Created = new DateTime(2021, 5, 15),
                     },
                    new Enrollment {
                        Id = 4,
                        StudentID = studentSecond.Id,
                        GroupID = firstGroup.Id,
                        Created = new DateTime(2021, 5, 29),
                       
                     },
                    new Enrollment {
                        Id = 5,
                        StudentID = studentThird.Id,
                        GroupID = secondGroup.Id,
                        Created = new DateTime(2021, 4, 17),
                     }
            });
        }
    }
}
