using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JarvisAuth.Application.Security.Attributes
{
    public class OnlyAdministratorAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var isAdminClaim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IsAdmin");

            if (isAdminClaim == null || isAdminClaim.Value != "True")
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
