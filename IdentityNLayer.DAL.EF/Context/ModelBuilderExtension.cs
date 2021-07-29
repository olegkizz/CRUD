using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityNLayer.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityNLayer.DAL.EF.Context
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            Group firstGroup = new Group
            {
                Id = 1,
                Number = "Nemiga-1",
                Status = GroupStatus.Started,
                TeacherId = 1
            };
            Group secondGroup = new Group
            {
                Id = 2,
                Number = "Nemiga-2",
                Status = GroupStatus.Pending,
                TeacherId = 1
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
