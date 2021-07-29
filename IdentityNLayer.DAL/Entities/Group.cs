using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static IdentityNLayer.DAL.Entities.GroupStatus;
namespace IdentityNLayer.DAL.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public GroupStatus Status { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher{ get; set; }
    }
}