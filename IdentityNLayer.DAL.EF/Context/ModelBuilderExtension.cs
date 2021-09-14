using System;
using System.Collections.Generic;
using IdentityNLayer.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityNLayer.DAL.EF.Context
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var userManager = modelBuilder.Entity<Person>();
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
      
            List<Person> users = new()
            {
                new Person
                {
                        UserName = "admin@admin.com",
                        NormalizedUserName = "admin@admin.com".ToUpper(),
                        EmailConfirmed = true,
                        Email = "admin@admin.com",
                        BirthDate = new DateTime(1998, 9, 21),
                        FirstName = "admin",
                        LastName = "admin"
                },
                new Person
                {
                        UserName = "manager@manager.com",
                        NormalizedUserName = "manager@manager.com".ToUpper(),
                        EmailConfirmed = true,
                        Email = "manager@manager.com",
                        BirthDate = new DateTime(2000, 9, 21),
                    FirstName = "manager",
                    LastName = "manager"
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
            for (int i = 1; i < 20; ++i)
                users.Add(new Person
                {
                    UserName = "guest" + i + "@mail.com",
                    NormalizedUserName = "guest" + i + "@mail.com".ToUpper(),
                    EmailConfirmed = true,
                    Email = "guest" + i + "@mail.com",
                    BirthDate = new DateTime(1999, 3, 22),
                    FirstName = "guest" + i,
                    LastName = "standart"
                });
            foreach (Person user in users)
            {
                user.PasswordHash = new PasswordHasher<Person>().HashPassword(user, "Kiselev12-");
                userManager.HasData(user);
                foreach (IdentityRole role in rolesIdentity)
                {
                    if (user.UserName.Contains(role.Name.ToLower()))
                    {
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
                Description = "Super MVC",
                CourseId = 1
            };
            Topic topic2 = new Topic()
            {
                Id = 2,
                Title = "Spring",
                Description = "Super Spring",
                CourseId = 2
            };
            Topic topic3 = new Topic()
            {
                Id = 3,
                Title = "ReactJS",
                Description = "Super ReactJS",
                CourseId = 3
            };    
            Topic topic4 = new Topic()
            {
                Id = 4,
                Title = "AngularJS",
                Description = "Super AngularJS",
                CourseId = 3
            };  
            Topic topic5 = new Topic()
            {
                Id = 5,
                Title = "NodeJS",
                Description = "Super NodeJS",
                CourseId = 3
            };
            modelBuilder.Entity<Topic>().HasData(
             new Topic[]
             {
                    topic1, topic2, topic3, topic4, topic5
             });
            Course course1 = new Course()
            {
                Id = 1,
                Title = "ASP",
                Description = "Super MVC",
            };
            Course course2 = new Course()
            {
                Id = 2,
                Title = "Java",
                Description = "Super Spring",
            };
            Course course3 = new Course()
            {
                Id = 3,
                Title = "JavaScript",
                Description = "Super JavaScript",
            };
            modelBuilder.Entity<Course>().Property(en => en.Updated).HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Course>().HasData(
                    new Course[]
                    {
                        course1, course2, course3
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
