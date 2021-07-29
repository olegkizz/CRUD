using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.DTO
{
    public class TeacherDTO
    {
        public int Id;
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CareerStart { get; set; }
    }
}
