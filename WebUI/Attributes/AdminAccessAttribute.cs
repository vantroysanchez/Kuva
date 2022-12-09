using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebUI.Attributes
{
    public class AdminAccessAttribute : IAuthorizationRequirement
    {
    }

    public class AdminAccessHandler : AuthorizationHandler<AdminAccessAttribute>
    {
        private readonly ICurrentUserService _currentUserService;

        public AdminAccessHandler(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminAccessAttribute requirement)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var role = _currentUserService.Roles;
                if (!string.IsNullOrEmpty(role))
                {
                    role.Equals("Administrator");

                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            return Task.CompletedTask;
        }
    }
}
