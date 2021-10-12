using AutoMapper;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.Core.Filters;
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
            CreateMap<StudentMark, StudentMarkModel>()
               .ReverseMap();
            CreateMap<Group, GroupModel>()
                .ReverseMap();
            CreateMap<Course, CourseModel>()
                .ReverseMap();
            CreateMap<GroupLesson, GroupLessonModel>()
                .ReverseMap();
            CreateMap<Lesson, LessonModel>()
               .ReverseMap();
            CreateMap<Methodist, MethodistModel>()
                .ReverseMap();
            CreateMap<MethodistRegisterModel, Methodist>()
             .ReverseMap();
            CreateMap<CourseFilter, CourseFilterModel>()
            .ReverseMap();
        }
    }
}