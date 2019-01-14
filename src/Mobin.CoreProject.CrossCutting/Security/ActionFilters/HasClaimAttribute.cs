using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Alamut.Data.Structure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mobin.CoreProject.CrossCutting.Security.ActionFilters
{
    public class HasClaimAttribute : ActionFilterAttribute
    {
        private readonly string _claim;
        private readonly string _paramName;

        public HasClaimAttribute(object claim, string paramName = "id")
        {
            _claim = claim.ToString();
            _paramName = paramName;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.TryGetValue(_paramName, out var paramValue))
            { throw new ArgumentException($"could not find argument with name {_paramName}"); }

            if (context.HttpContext.User.HasClaim(_claim, paramValue.ToString()))
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
