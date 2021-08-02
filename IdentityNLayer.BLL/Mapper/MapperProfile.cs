using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityNLayer.BLL.DTO;
using AutoMapper;
using IdentityNLayer.DAL.Entities;

namespace IdentityNLayer.BLL.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Teacher, TeacherDTO>()
                .ReverseMap();
            CreateMap<Student, StudentDTO>()
                .ReverseMap();
            CreateMap<Group, GroupDTO>()
                .ReverseMap();
        }
    }
}