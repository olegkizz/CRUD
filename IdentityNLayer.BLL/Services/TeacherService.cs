using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IdentityNLayer.BLL.DTO;
using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.DAL.Entities;
using IdentityNLayer.DAL.Interfaces;

namespace IdentityNLayer.BLL.Services
{
    public class TeacherService : ITeacherService
    {
        private IUnitOfWork Db { get; set; }

        public TeacherService(IUnitOfWork db)
        {
            Db = db;
        }

        public IEnumerable<TeacherDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Teacher, TeacherDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Teacher>, List<TeacherDTO>>(Db.Teachers.GetAll());
        }
    }
}
