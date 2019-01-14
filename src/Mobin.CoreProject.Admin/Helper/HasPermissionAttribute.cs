using System;
using System.Linq;
using System.Net;
using Alamut.Data.Structure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Mobin.CoreProject.Core.SSOT;
using Mobin.CoreProject.CrossCutting.Security.Helper;

namespace Mobin.CoreProject.Admin.Helper
{
    //public class HasPermissionAttribute : ActionFilterAttribute
    //{
    //    private readonly Permissions[] _permissions;

    //    public HasPermissionAttribute(params Permissions[] permissions)
    //    {
    //        _permissions = permissions;
    //    }

    //    public override void OnActionExecuting(ActionExecutingContext context)
    //    {
    //        var stringedPermissions = _permissions.Select(s => s.ToString());

    //        if (context.HttpContext.User.HasPermissions(stringedPermissions))
    //        {
    //            base.OnActionExecuting(context);
    //        }
    //        else
    //        {
    //            context.Result = new JsonResult(ServiceResult.Error("you are not authorized", (int) HttpStatusCode.Forbidden));
    //        }
    //    }
    //}

    //public class HasClaimAttribute : ActionFilterAttribute
    //{
    //    private readonly Claims _claim;
    //    private readonly string _paramName;

    //    public HasClaimAttribute(Claims claim, string parameterName = "id")
    //    {
    //        _claim = claim;
    //        _paramName = parameterName;
    //    }

    //    public override void OnActionExecuting(ActionExecutingContext context)
    //    {
    //        if (!context.ActionArguments.TryGetValue(_paramName, out var paramValue))
    //            { throw new ArgumentException($"could not find argument with name {_paramName}"); }

    //        if (context.HttpContext.User.HasClaim(_claim.ToString(), paramValue.ToString()))
    //        {
    //            base.OnActionExecuting(context);
    //        }
    //        else
    //        {
    //            context.Result = new JsonResult(ServiceResult.Error("you are not authorized", (int) HttpStatusCode.Forbidden));
    //        }
    //    }
    //}
    
}