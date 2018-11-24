using System.Net;
using Alamut.Data.Structure;
using Alamut.Helpers.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Mobin.CoreProject.Core.SSOT;

namespace Mobin.CoreProject.Admin.Helper
{
    public class HasPermissionAttribute : ActionFilterAttribute
    {
        //private readonly Permissions _permission;
        private readonly Permissions[] _permissions;

        //public HasPermissionAttribute(Permissions permission)
        //{
        //    _permission = permission;
        //}

        public HasPermissionAttribute(params Permissions[] permissions)
        {
            _permissions = permissions;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.HasPermissions(_permissions))
            {
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = new JsonResult(ServiceResult.Error("you are not authorized", (int) HttpStatusCode.Forbidden));
            }

            //if (_permissions.IsAny())
            //{
            //    if (context.HttpContext.User.HasPermissions(_permissions))
            //    {
            //        base.OnActionExecuting(context);
            //    }
            //    else
            //    {
            //        context.Result = new JsonResult(ServiceResult.Error("you are not authorized", (int) HttpStatusCode.Forbidden));
            //    }
            //}
            //else if (context.HttpContext.User.HasPermission(_permission))
            //{
            //    base.OnActionExecuting(context);
            //}
            //else
            //{
            //    context.Result = new JsonResult(ServiceResult.Error("you are not authorized", (int) HttpStatusCode.Forbidden));
            //}
        }
    }
    
}