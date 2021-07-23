using System.Threading.Tasks;
using CRUD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace CRUD.Authorization
{
    public class ContactAdministratorsAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Contact>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            Contact resource)
        {
            // Administrators can do anything.
            if (context.User.IsInRole(Constants.ContactAdministratorsRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}