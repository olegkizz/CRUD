using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityNLayer.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
                Name = "Teach1",
                Email = "Teach1@teacher.com",
                CareerStart = new DateTime(2015, 7, 20)
            };
            Teacher teacher2 = new Teacher
            {
                Id = 2,
                Name = "Teach2",
                Email = "Teach2@teacher.com",
                CareerStart = new DateTime(2017, 2, 21)
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
                TeacherId = teacher1.Id
            };
            Group secondGroup = new Group
            {
                Id = 2,
                Number = "Nemiga-2",
                Status = GroupStatus.Pending,
                TeacherId = teacher2.Id
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
                        Id = 1, Name = "Oleg", GroupId = firstGroup.Id, Type = StudentType.Online
                    },
                    new Student
                    {
                        Id = 2, Name = "Vova",  GroupId = firstGroup.Id, Type = StudentType.Offline
                    },
                    new Student
                    {
                        Id = 3, Name = "Nikita", GroupId = secondGroup.Id,  Type = StudentType.Mix
                    }
                }
            );
        }
    }
}
