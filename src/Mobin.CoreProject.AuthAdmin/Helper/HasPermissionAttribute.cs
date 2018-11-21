using System.Net;
using System.Security.Claims;
using Alamut.Data.Structure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Mobin.CoreProject.Core.SSOT;

namespace Mobin.CoreProject.AuthAdmin.Helper
{
    public class HasPermissionAttribute : ActionFilterAttribute
    {
        private readonly Permissions _permission;

        public HasPermissionAttribute(Permissions permission)
        {
            _permission = permission;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.HasPermission(_permission))
            {
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = new JsonResult(ServiceResult.Error("you are not authorized", (int) HttpStatusCode.Forbidden));
            }
        }
    }


    public static class ClaimsPrincipalExtensions
    {
        public static bool HasPermission(this ClaimsPrincipal principal, Permissions permission) => 
            principal.HasClaim(AlamutClaimTypes.Permission, permission.ToString());
    }
}