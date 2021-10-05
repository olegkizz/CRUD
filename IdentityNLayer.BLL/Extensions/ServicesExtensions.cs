using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityNLayer.BLL.Extensions
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
            services.AddScoped<IMethodistService,
             MethodistService>();
            services.AddScoped<IEmailService,
             EmailService>();
            return services;
        }
    }
}
