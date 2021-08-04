using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Threading.Tasks;
using Constants = IdentityNLayer.Authorization.Constants;
using Microsoft.AspNetCore.Identity;
using IdentityNLayer.Core.Entities;

namespace IdentityNLayer.BLL.Authorization
{
    public class ContactManagerAuthorizationHandler : 
        AuthorizationHandler<OperationAuthorizationRequirement, Student>
    {
        UserManager<IdentityUser> _userManager;

        public ContactManagerAuthorizationHandler(UserManager<IdentityUser>
            userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            Student resource)
        {
            if (resource == null)
            {
                return Task.CompletedTask;
            }

            // If not asking for approval/reject, return.
            if (requirement.Name != Constants.ApproveOperationName &&
                requirement.Name != Constants.RejectOperationName)
            {
                return Task.CompletedTask;
            }

            // Managers can approve or reject.
            if (context.User.IsInRole(Constants.ContactManagersRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}