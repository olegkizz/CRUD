using System;
using System.Collections.Generic;
using IdentityNLayer.Core.Entities;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface IStudentService : IBaseService<Student>
    {
        public Array GetStudentTypes();
        public List<Group> GetStudentGroups(int studentId);
        public void Enrol(int studentId, int groupdId, bool confirmed = true);
    }
}
