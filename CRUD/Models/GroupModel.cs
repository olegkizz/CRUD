using IdentityNLayer.Core.Entities;
using System;
using System.Collections.Generic;

namespace IdentityNLayer.Models
{
    public class GroupModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Status { get; set; }
        public int TeacherId { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
