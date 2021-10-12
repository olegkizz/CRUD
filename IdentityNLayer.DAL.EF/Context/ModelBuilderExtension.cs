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
            string[] roles = { "Admin", "Methodist",
                "Student", "Teacher"
            };
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
                }
            };
            for (int i = 0; i < 25; ++i)
            {
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
            }
            int j = 0;
            List<Student> students = new();
            List<Teacher> teachers = new();
            List<Methodist> methodists = new();
            foreach (Person user in users)
            {
                user.PasswordHash = new PasswordHasher<Person>().HashPassword(user, "Kiselev12-");
                userManager.HasData(user);
                if(j == 0)
                {
                    foreach (IdentityRole roleIdentity in rolesIdentity)
                    {
                        if (roleIdentity.Name == "Admin")
                        {
                            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
                            {
                                RoleId = roleIdentity.Id,
                                UserId = user.Id
                            });
                        }
                    }
                } else if (j <= 11)
                {
                    foreach (IdentityRole roleIdentity in rolesIdentity)
                    {
                        if (roleIdentity.Name == "Student")
                        {
                            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
                            {
                                RoleId = roleIdentity.Id,
                                UserId = user.Id
                            });
                            Student student = new Student()
                            {
                                Id = j,
                                Type = StudentType.Online,
                                UserId = user.Id
                            };
                            students.Add(student);
                        }
                    }
                }
                else if (j > 11 && j <= 16)
                {
                    foreach (IdentityRole roleIdentity in rolesIdentity)
                    {
                        if (roleIdentity.Name == "Teacher")
                        {
                            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
                            {
                                RoleId = roleIdentity.Id,
                                UserId = user.Id
                            });
                            Teacher teacher = new Teacher
                            {
                                Id = j,
                                Bio = $"Teacher{j}. Programmist.",
                                LinkToProfile = "https://github.com",
                                UserId = user.Id
                            };
                            teachers.Add(teacher);
                        }
                    }
                }
                else if (j > 16 && j <= 21)
                {
                    foreach (IdentityRole roleIdentity in rolesIdentity)
                    {
                        if (roleIdentity.Name == "Methodist")
                        {
                            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
                            {
                                RoleId = roleIdentity.Id,
                                UserId = user.Id
                            });
                            Methodist methodist = new Methodist
                            {
                                Id = j,
                                LinkToContact = "https://web.telegram.org",
                                UserId = user.Id
                            };
                            methodists.Add(methodist);
                        }
                    }
                }
                j++;
            }
            modelBuilder.Entity<Student>().HasData(students);
            modelBuilder.Entity<Teacher>().HasData(teachers);
            modelBuilder.Entity<Methodist>().HasData(methodists);
            {
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
                Topic topic3 = new Topic()
                {
                    Id = 3,
                    Title = "ReactJS",
                    Description = "Super ReactJS"
                };
                Topic topic4 = new Topic()
                {
                    Id = 4,
                    Title = "AngularJS",
                    Description = "Super AngularJS"
                };
                Topic topic5 = new Topic()
                {
                    Id = 5,
                    Title = "PythonBackend",
                    Description = "Super PythonBackend"
                };
                Topic topic6 = new Topic()
                {
                    Id = 6,
                    Title = "PHP Magento",
                    Description = "Super Magento"
                };
                Topic topic7 = new Topic()
                {
                    Id = 7,
                    Title = "PHP WordPress",
                    Description = "Super WordPress"
                };
                modelBuilder.Entity<Topic>().HasData(
                         new Topic[]
                         {
                    topic1, topic2, topic3, topic4, topic5, topic6, topic7
                });
                Course course1 = new Course()
                {
                    Id = 1,
                    Title = "ASP",
                    Description = "Super MVC",
                    TopicId = 1
                };
                Course course2 = new Course()
                {
                    Id = 2,
                    Title = "Java",
                    Description = "Super Spring",
                    TopicId = 2
                };
                Course course3 = new Course()
                {
                    Id = 3,
                    Title = "JavaScript",
                    Description = "Super JavaScript",
                    TopicId = 3
                };
                Course course4 = new Course()
                {
                    Id = 4,
                    Title = "Python",
                    Description = "Super Python",
                    TopicId = 5
                };
                Course course5 = new Course()
                {
                    Id = 5,
                    Title = "PHP",
                    Description = "Super PHP",
                    TopicId = 6
                };
                Course course6 = new Course()
                {
                    Id = 6,
                    Title = "PHP",
                    Description = "Super PHP",
                    TopicId = 7
                };
                modelBuilder.Entity<Course>().Property(en => en.Updated).HasDefaultValueSql("getdate()");

                List<Course> courses = new()
                {
                    course1,
                    course2,
                    course3,
                    course4,
                    course5,
                    course6
                };
                modelBuilder.Entity<Course>().HasData(courses);
                Lesson lesson1 = new()
                {
                    Id = 1,
                    Name = "Lesson 1",
                    Theme = "Super Lesson 1",
                    CourseId = course1.Id,
                    Duration = 5
                };
                Lesson lesson2 = new Lesson()
                {
                    Id = 2,
                    Name = "Lesson 2",
                    Theme = "Super Lesson 2",
                    CourseId = course1.Id,
                    Duration = 5
                };
                Lesson lesson3 = new Lesson()
                {
                    Id = 3,
                    Name = "Lesson 3",
                    Theme = "Super Lesson 3",
                    CourseId = course1.Id,
                    Duration = 5
                };
                Lesson lesson4 = new Lesson()
                {
                    Id = 4,
                    Name = "Lesson 1",
                    Theme = "Super Lesson 1",
                    CourseId = course2.Id,
                    Duration = 1
                };
                Lesson lesson5 = new Lesson()
                {
                    Id = 5,
                    Name = "Lesson 2",
                    Theme = "Super Lesson 2",
                    CourseId = course2.Id,
                    Duration = 1
                };
                Lesson lesson6 = new Lesson()
                {
                    Id = 6,
                    Name = "Lesson 3",
                    Theme = "Super Lesson 3",
                    CourseId = course2.Id,
                    Duration = 1
                };
                modelBuilder.Entity<Lesson>().Property(l => l.Updated).HasDefaultValueSql("getdate()");

                modelBuilder.Entity<Lesson>().HasData(
                        new Lesson[]
                        {
                        lesson1, lesson2, lesson3, lesson4, lesson5, lesson6
                        });
                Group group1 = new Group
                {
                    Id = 1,
                    Number = "ASP-1",
                    Status = GroupStatus.Pending,
                    TeacherId = teachers[0].Id,
                    StartDate = new DateTime(2021, 9, 21),
                    CourseId = course1.Id,
                    MethodistId = methodists[0].Id
                };
                Group group2 = new Group
                {
                    Id = 2,
                    Number = "Java-1",
                    Status = GroupStatus.Pending,
                    TeacherId = teachers[1].Id,
                    StartDate = new DateTime(2021, 10, 21),
                    CourseId = course2.Id,
                    MethodistId = methodists[1].Id
                };
                Group group3 = new Group
                {
                    Id = 3,
                    Number = "JavaScript-1",
                    Status = GroupStatus.Pending,
                    TeacherId = teachers[2].Id,
                    StartDate = new DateTime(2021, 10, 21),
                    CourseId = course3.Id,
                    MethodistId = methodists[2].Id
                };
                Group group4 = new Group
                {
                    Id = 4,
                    Number = "Python-1",
                    Status = GroupStatus.Pending,
                    TeacherId = teachers[3].Id,
                    StartDate = new DateTime(2021, 10, 21),
                    CourseId = course4.Id,
                    MethodistId = methodists[3].Id
                };
                Group group5 = new Group
                {
                    Id = 5,
                    Number = "PHP-1",
                    Status = GroupStatus.Pending,
                    TeacherId = teachers[4].Id,
                    StartDate = new DateTime(2021, 10, 21),
                    CourseId = course5.Id,
                    MethodistId = methodists[4].Id
                };
                List<Group> groups = new()
                {
                    group1,
                    group2,
                    group3,
                    group4,
                    group5
                };
                modelBuilder.Entity<Group>().HasData(groups);
                Enrollment enrollment = new();
                List<Enrollment> enrollments = new();
                int courseId = 0;
                int groupId = 0;

                for (int i = 1; i <= 25; i++)
                {
                    if (i <= 11)
                    {
                        if (courseId > 3)
                            courseId = 0;
                        enrollments.Add(new Enrollment
                        {
                            Id = i,
                            UserID = users[i].Id,
                            EntityID = courses[courseId].Id,
                            State = UserGroupStates.Requested,
                            Role = UserRoles.Student
                        });
                        courseId++;
                    }
                    else if (i > 11 && i <= 16)
                    {
                        enrollments.Add(new Enrollment
                        {
                            Id = i,
                            UserID = users[i].Id,
                            EntityID = groups[groupId].Id,
                            State = UserGroupStates.Applied,
                            Role = UserRoles.Teacher
                        });
                        groupId++;
                    }
                }
                modelBuilder.Entity<Enrollment>().Property(en => en.Updated).HasDefaultValueSql("getdate()");
                modelBuilder.Entity<Enrollment>().HasData(enrollments);
            }
        }
    }
}
