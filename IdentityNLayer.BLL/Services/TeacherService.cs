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
        private IMapper _mapper { get; set; }
        public TeacherService(IUnitOfWork db, IMapper mapper = null)
        {
            Db = db;
            _mapper = mapper;
        }

        public IEnumerable<TeacherDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<Teacher>, List<TeacherDTO>>(Db.Teachers.GetAll());
        }
        public TeacherDTO GetById(int id)
        { 
            return _mapper.Map<Teacher, TeacherDTO>(Db.Teachers.Get(id));
        }

        public void Create(TeacherDTO entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TeacherDTO entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
