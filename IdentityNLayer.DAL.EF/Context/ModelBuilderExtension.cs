using System;
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
            Group firstGroup = new Group
            {
                Id = 1,
                Number = "Nemiga-1",
                Status = GroupStatus.Started,
                TeacherId = teacher1.Id,
                StartDate = new DateTime(2021, 9, 21)
            };
            Group secondGroup = new Group
            {
                Id = 2,
                Number = "Nemiga-2",
                Status = GroupStatus.Pending,
                TeacherId = teacher2.Id,
                StartDate = new DateTime(2021, 10, 21)
            };
            modelBuilder.Entity<Group>().HasData(
                new Group[]
                {
                    firstGroup, secondGroup
                }
            );
            modelBuilder.Entity<Student>().HasData(
                new Student[]
                {
                    new Student
                    {
                        Id = 1, FirstName = "Oleg", LastName = "Kizz", GroupId = firstGroup.Id, Type = StudentType.Online
                    },
                    new Student
                    {
                        Id = 2, FirstName = "Vova", LastName = "Braslav",  GroupId = firstGroup.Id, Type = StudentType.Offline
                    },
                    new Student
                    {
                        Id = 3, FirstName = "Nikita", LastName = "Chebur", GroupId = secondGroup.Id,  Type = StudentType.Mix
                    }
                }
            );
        }
    }
}
