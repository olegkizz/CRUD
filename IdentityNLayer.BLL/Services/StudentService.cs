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
    public class StudentService : IStudentService
    {
        private IUnitOfWork Db { get; set; }
        private readonly IMapper _mapper;
        public StudentService(IUnitOfWork db, IMapper mapper)
        {
            Db = db;
            _mapper = mapper;
        }
        public IEnumerable<StudentDTO> GetAll()
        {
            return _mapper.Map<List<StudentDTO>>(Db.Students.GetAll());
        }
        public StudentDTO GetById(int id)
        {
            return _mapper.Map<StudentDTO>(Db.Students.Get(id));
        }
        public void Create(StudentDTO studentDto)
        {
            /*.ForMember("Group", opt
                => opt.MapFrom(st => Db.Students.Get(st.GroupId)))*/
            Db.Students.Create(_mapper.Map<StudentDTO, Student>(studentDto));
            Db.Save();
        }

        public Array GetStudentTypes()
        {
            return Enum.GetValues(typeof(StudentType));
        }

        public void Update(StudentDTO entity)
        {
            Db.Students.Update(_mapper.Map<StudentDTO, Student>(entity));
            Db.Save();
        }

        public void Delete(int id)
        {
            Db.Students.Delete(id);
            Db.Save();
        }
    }
}
