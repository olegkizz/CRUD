using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.BLL.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentService,
                StudentService>();
            services.AddScoped<IGroupService,
                GroupService>();
            services.AddScoped<ITeacherService,
                TeacherService>();
            services.AddScoped<IEnrollmentService,
              EnrollmentService>();
            services.AddScoped<ICourseService,
              CourseService>();
            services.AddScoped<IFileService,
              FileService>();
            services.AddScoped<ILessonService,
              LessonService>();
            services.AddScoped<IGroupLessonService,
              GroupLessonService>();
            services.AddScoped<IStudentMarkService,
             StudentMarkService>();
            services.AddScoped<ITopicService,
             TopicService>();
            return services;
        }
    }
}
