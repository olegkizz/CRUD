using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityNLayer.Models
{
    public class GroupModel
    {
        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        public GroupStatus Status { get; set; }
        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public DateTime? StartDate { get; set; }
      
        public List<StudentRequestsModel> StudentRequests { get; set; }
        public void SetStudents(IEnumerable<StudentModel> students, IGroupService _groupService)
        {
            List<StudentRequestsModel> studentRequests = new();
            if (students != null)
                foreach (StudentModel student in students)
                {
                    studentRequests.Add(new StudentRequestsModel()
                    {
                        UserName = student.Name,
                        UserId = student.UserId,
                        Applied = _groupService.HasStudent(Id, student.UserId) ? true : false
                    });
                }
            StudentRequests = studentRequests;
        }
    }
}
