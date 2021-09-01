using IdentityNLayer.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityNLayer.Middleware
{
    public class CreatingAccountMiddleware
    {
        private RequestDelegate _next;
   

        public CreatingAccountMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, 
            IStudentService studentService,
            UserManager<IdentityUser> userManager,
            ITeacherService teacherService)
        { 
            var courseId = context.Request.Query["courseId"];
            
            if (context.Request.HasFormContentType)
            {
                if (context.Request.Method.ToLower() == "post" && context.Request.Form["request"] == "Yes")
                {
                    if (context.Request.Path == "/Students/SendRequest")
                    {
                        if (!studentService.HasAccount(userManager.GetUserAsync(context.User).Result.Id))
                            context.Response.Redirect("/Students/Create?courseId=" + courseId);
                        else await _next.Invoke(context);
                    }
                    else if (context.Request.Path == "/Teachers/SendRequest")
                    {
                        if (!teacherService.HasAccount(userManager.GetUserAsync(context.User).Result.Id))
                            context.Response.Redirect("/Teachers/Create?courseId=" + courseId);
                        else await _next.Invoke(context);
                    }
                    else
                    {
                        await _next.Invoke(context);
                    }
                }
                else
                {
                    await _next.Invoke(context);
                }
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
