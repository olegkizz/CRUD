using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace IdentityNLayer.Models
{
    public class GroupModel
    {
        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        [Remote(action: "VerifyStartGroup", controller: "Groups", AdditionalFields = nameof(Id))]
        public GroupStatus Status { get; set; }
        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int? MethodistId { get; set; }
        public Methodist Methodist { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public DateTime? StartDate { get; set; }

        public List<StudentRequestsModel> StudentRequests { get; set; }
        public async Task<IEnumerable<StudentRequestsModel>> SetStudents(IEnumerable<StudentModel> students, IGroupService _groupService)
        {
            List<StudentRequestsModel> studentRequests = new();
            if (students != null)
                foreach (StudentModel student in students)
                {
                    if (student != null)
                        studentRequests.Add(new StudentRequestsModel()
                        {
                            UserName = student.Name,
                            UserId = student.UserId,
                            Applied = await _groupService.HasStudent(Id, student.UserId) ? true : false
                        });
                }
            StudentRequests = studentRequests;
            return studentRequests;
        }
    }
}
