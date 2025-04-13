using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace web_api.Services
{
    public class PermissionFilter : IAuthorizationFilter
    {
        private readonly string _requiredPermission;

        public PermissionFilter(string requiredPermission)
        {
            _requiredPermission = requiredPermission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userPermission = user.FindFirst("PermissionLevel")?.Value;
            if (userPermission == null || userPermission != _requiredPermission)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}