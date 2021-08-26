using System;
using System.Collections.Generic;
using IdentityNLayer.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityNLayer.DAL.EF.Context
{
    public static class ModelBuilderExtension
    {
        public async static void Seed(this ModelBuilder modelBuilder)
        {
            var userManager = modelBuilder.Entity<IdentityUser>();
            var roleManager = modelBuilder.Entity<IdentityRole>();
            string[] roles = { "Admin", "Manager",
                "Student", "Teacher"
            };
            /*    RoleManager<IdentityRole> roleManager = new ();
                UserManager<IdentityUser> userManager = new ();*/
            List<IdentityRole> rolesIdentity = new();
            foreach (string role in roles)
            {
                IdentityRole roleIdentity = new IdentityRole()
                {
                    Name = role,
                    NormalizedName = role.ToUpper()
                };
                roleManager.HasData(roleIdentity);
                rolesIdentity.Add(roleIdentity);
            }

            IdentityUser[] users =
            {
                new IdentityUser
                    {
                        UserName = "admin@admin.com",
                        NormalizedUserName = "admin@admin.com".ToUpper(),
                        EmailConfirmed = true,
                        Email = "admin@admin.com"
                    },
                new IdentityUser
                    {
                        UserName = "manager@manager.com",
                        NormalizedUserName = "manager@manager.com".ToUpper(),
                        EmailConfirmed = true,
                        Email = "manager@manager.com"
                    },
               /* new IdentityUser
                    {
                        UserName = "studentfirst@mail.com",
                        NormalizedUserName = "studentfirst@mail.com".ToUpper(),
                        EmailConfirmed = true,
                        Email = "studentfirst@mail.com"
                    },  
                new IdentityUser
                    {
                        UserName = "studentsecond@mail.com",
                        NormalizedUserName = "studentsecond@mail.com".ToUpper(),
                        EmailConfirmed = true,
                        Email = "studentsecond@mail.com"
                    },  
                new IdentityUser
                    {
                        UserName = "studentthird@mail.com",
                        NormalizedUserName = "studentthird@mail.com".ToUpper(),
                        EmailConfirmed = true,
                        Email = "studentthird@mail.com"
                    },
                 
                new IdentityUser
                    {
                        UserName = "teacherfirst@mail.com",
                        NormalizedUserName = "teacherfirst@mail.com".ToUpper(),
                        EmailConfirmed = true,
                        Email = "teacherfirst@mail.com"
                    },
                new IdentityUser
                    {
                        UserName = "teachersecond@mail.com",
                        NormalizedUserName = "teachersecond@mail.com".ToUpper(),
                        EmailConfirmed = true,
                        Email = "teachersecond@mail.com"
                    },
                 new IdentityUser
                   {
                       UserName = "studentfourth@mail.com",
                       NormalizedUserName = "studentfourth@mail.com".ToUpper(),
                       EmailConfirmed = true,
                       Email = "studentfourth@mail.com"
                   },
                     new IdentityUser
                   {
                       UserName = "studentfifth@mail.com",
                       NormalizedUserName = "studentfifth@mail.com".ToUpper(),
                       EmailConfirmed = true,
                       Email = "studentfifth@mail.com"
                  },*/
            };           
            foreach(IdentityUser user in users)
            {
                foreach (IdentityRole role in rolesIdentity)
                {
                    if (user.UserName.Contains(role.Name.ToLower()))
                    {
                        user.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(user, "Kiselev12-");
                        userManager.HasData(user);
                        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
                        {
                            RoleId = role.Id,
                            UserId = user.Id
                        });
                    }
                }
            }
           
               /* Teacher teacher1 = new Teacher
                {
                    Id = 1,
                    FirstName = "Teach",
                    LastName = "First",
                    BirthDate = new DateTime(1985, 2, 21),
                    UserId = users[5].Id,
                    Bio = "Super Teacher"
                };
                Teacher teacher2 = new Teacher
                {
                    Id = 2,
                    FirstName = "Teach",
                    LastName = "Second",
                    BirthDate = new DateTime(1992, 2, 21),
                    UserId = users[6].Id,
                    Bio = "Super Teacher"
                };

                modelBuilder.Entity<Teacher>().HasData(
                   new Teacher[]
                   {
                    teacher1, teacher2
                   }
               );*/
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
                    Title = "ASP",
                    Description = "Super MVC",
                    TopicId = topic1.Id
                };
                Course course2 = new Course()
                {
                    Id = 2,
                    Title = "Java",
                    Description = "Super Spring",
                    TopicId = topic2.Id
                };
            modelBuilder.Entity<Course>().Property(en => en.Updated).HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Course>().HasData(
                    new Course[]
                    {
                        course1, course2
                    });
      /*      Group firstGroup = new Group
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
            );*/
       /*     Student studentFirst = new Student
                {
                    Id = 1,
                    FirstName = "Oleg",
                    LastName = "Kizz",
                    UserId = users[2].Id,
                    BirthDate = new DateTime(2005, 12, 11),
                    Type = StudentType.Online
                };
                Student studentSecond = new Student
                {
                    Id = 2,
                    FirstName = "Vova",
                    LastName = "Braslav",
                    UserId = users[3].Id,
                    BirthDate = new DateTime(2006, 10, 11),
                    Type = StudentType.Offline
                };
                Student studentThird = new Student
                {
                    Id = 3,
                    FirstName = "Nikita",
                    LastName = "Chebur",
                    UserId = users[4].Id,
                    BirthDate = new DateTime(2005, 04, 12),
                    Type = StudentType.Mix
                };
                Student studentFourth = new Student
                {
                    Id = 4,
                    FirstName = "Mikola",
                    LastName = "Cool",
                    UserId = users[7].Id,
                    BirthDate = new DateTime(2005, 04, 12),
                    Type = StudentType.Online
                };
                Student studentFifth = new Student
                {
                    Id = 5,
                    FirstName = "Vovka",
                    LastName = "Sabur",
                    UserId = users[8].Id,
                    BirthDate = new DateTime(2005, 04, 12),
                    Type = StudentType.Mix
                };
            modelBuilder.Entity<Student>().HasData(
                     new Student[]
                     {
                        studentFirst, studentSecond, studentThird, studentFourth, studentFifth
                     }
                );*/
                modelBuilder.Entity<Enrollment>().Property(en => en.Updated).HasDefaultValueSql("getdate()");
                /*modelBuilder.Entity<Enrollment>().HasData(
                    new Enrollment[]
                    {
                        new Enrollment {
                            Id = 1,
                            UserID = users[2].Id,
                            GroupID = firstGroup.Id,
                        },
                         new Enrollment {
                            Id = 2,
                            UserID = users[2].Id,
                            GroupID = secondGroup.Id,
                         },
                        new Enrollment {
                            Id = 3,
                            UserID = users[3].Id,
                            GroupID = secondGroup.Id,
                         },
                        new Enrollment {
                            Id = 4,
                            UserID = users[3].Id,
                            GroupID = firstGroup.Id,
                         },
                        new Enrollment {
                            Id = 5,
                            UserID = users[4].Id,
                            GroupID = secondGroup.Id,
                         }
                });*/
        }
    }
}
