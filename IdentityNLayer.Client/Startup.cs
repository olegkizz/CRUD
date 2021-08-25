using IdentityNLayer.BLL.Services;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.DAL.EF.Repositories;
using IdentityNLayer.DAL.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityNLayer.Client
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
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            services.AddDbContext<ApplicationContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
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
            services.AddScoped<IRepository<Course>, CoursesRepository>();
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.AddScoped<ICourseService, CourseService>();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
