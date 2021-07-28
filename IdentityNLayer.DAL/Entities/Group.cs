using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityNLayer.DAL.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string Name { get; set; }
    }
}