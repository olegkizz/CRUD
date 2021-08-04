using System;
using IdentityNLayer.Core.Entities;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface IStudentService : IService<Student>
    {
        public Array GetStudentTypes();
    }
}
