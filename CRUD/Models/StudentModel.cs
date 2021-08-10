using IdentityNLayer.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityNLayer.Models
{
    public class StudentModel : PersonModel
    {
        public string Type { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public int AssignGroupId { get; set; }
        [BindProperty]
        public List<AssignGroupModel> AssignGroups { get; set; }

        public StudentModel() { }
        public StudentModel(IEnumerable<Group> groups = null)
        {
            List<AssignGroupModel> assignGroups = new List<AssignGroupModel>();
            if(groups != null)
                foreach (Group gr in groups)
                {
                    assignGroups.Add(new AssignGroupModel()
                    {
                        GroupID = gr.Id,
                        Number = gr.Number,
                        Assigned = false
                    });
                }
            AssignGroups = assignGroups;
        }
    }
}
