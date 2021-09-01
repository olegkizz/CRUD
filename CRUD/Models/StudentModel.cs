using IdentityNLayer.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace IdentityNLayer.Models
{
    public class StudentModel : PersonModel
    {
        public string Type { get; set; }
        /*   public List<AssignGroupModel> AssignGroups { get; set; }*/
        public string[] StudentTypes { get; set; }
        public StudentModel()
        {
            StudentTypes = Enum.GetNames(typeof(StudentType));
        }
        /*        public StudentModel CreateAssignGroups(IEnumerable<Group> groups = null)
                {
                    List<AssignGroupModel> assignGroups = new List<AssignGroupModel>();
                    if (groups != null)
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
                    return this;
                }*/
    }
}
