using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityNLayer.DAL.Entities;

namespace IdentityNLayer.BLL.DTO
{
    public class GroupDTO
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public GroupStatus Status { get; set; }
        public int TeacherId { get; set; }
    }
}