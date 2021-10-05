using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.EF.Repositories;
using IdentityNLayer.DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNLayer.DAL.EF
{
    public static class RepositoriesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork,
              EFUnitOfWork>();
            services.AddTransient<IRepository<Student>,
                StudentsRepository>();
            services.AddTransient<IRepository<Group>,
                GroupsRepository>();
            services.AddTransient<IRepository<Teacher>,
                TeachersRepository>();
            services.AddTransient<IRepository<Enrollment>,
                EnrollmentsRepository>();
            services.AddTransient<IRepository<Course>,
                CoursesRepository>();
            services.AddTransient<IRepository<Topic>,
                TopicsRepository>();
            services.AddTransient<IRepository<Lesson>,
               LessonsRepository>();
            services.AddTransient<IRepository<GroupLesson>,
               GroupLessonsRepository>();
            services.AddTransient<IRepository<File>,
               FilesRepository>();
            services.AddTransient<IRepository<Topic>,
               TopicsRepository>();
            services.AddTransient<IRepository<StudentMark>,
              StudentMarksRepository>();
            services.AddTransient<IRepository<Methodist>,
              MethodistsRepository>();

            return services;
        }
    }
}
