using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using IdentityNLayer.BLL.Authorization;
using IdentityNLayer.BLL.Services;
using IdentityNLayer.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.DAL.EF.Repositories;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.BLL.Mapper;
using Microsoft.OpenApi.Models;
using System;

namespace IdentityNLayer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

       
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();
            services.AddControllersWithViews(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });
            // Authorization handlers.
            services.AddScoped<IAuthorizationHandler,
                ContactIsOwnerAuthorizationHandler>();

            services.AddScoped<IAuthorizationHandler,
                ContactAdministratorsAuthorizationHandler>();

            services.AddScoped<IAuthorizationHandler,
                ContactManagerAuthorizationHandler>();

            services.AddTransient<IUnitOfWork,
                EFUnitOfWork>();
            services.AddTransient<IRepository<Student>,
                StudentsRepository>();
            services.AddTransient<IRepository<Group>,
                GroupsRepository>();
            services.AddTransient<IRepository<Teacher>,
                TeachersRepository>();
            services.AddTransient<IRepository<StudentToGroupAction>,
                StudentToGroupActionsRepository>();
            services.AddTransient<IRepository<Enrollment>,
                EnrollmentsRepository>();
            services.AddTransient<IRepository<Course>,
                CoursesRepository>();
            services.AddTransient<IRepository<Topic>,
                TopicsRepository>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

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
            services.AddScoped<IStudentToGroupActionService,
              StudentToGroupActionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
