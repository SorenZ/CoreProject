using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Alamut.Data.Structure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Mobin.CoreProject.CrossCutting.Security.Helper;

namespace Mobin.CoreProject.CrossCutting.Security.ActionFilters
{
    public class HasPermissionAttribute : ActionFilterAttribute
    {
        private readonly IEnumerable<string> _permissions;
        
        public HasPermissionAttribute(params object[] permissions)
        {
            _permissions = permissions.Select(s => s.ToString());
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //var stringedPermissions = _permissions.Select(s => s.ToString());

            if (context.HttpContext.User.HasPermissions(_permissions))
            {
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = new JsonResult(ServiceResult.Error("you are not authorized", (int) HttpStatusCode.Forbidden));
            }
        }
    }
}
