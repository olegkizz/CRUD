using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IdentityNLayer.BLL.DTO;
using IdentityNLayer.DAL.Entities;
using IdentityNLayer.DAL.Interfaces;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface IStudentService : IService<StudentDTO>
    {
        public Array GetStudentTypes();
    }
}
