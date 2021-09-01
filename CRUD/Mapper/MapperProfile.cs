using AutoMapper;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.Models;

namespace IdentityNLayer.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Teacher, TeacherModel>()
                .ReverseMap();
            CreateMap<Student, StudentModel>()
                .ReverseMap();
            CreateMap<Group, GroupModel>()
                .ReverseMap();
            CreateMap<Course, CourseModel>()
                .ReverseMap();
        }
    }
}