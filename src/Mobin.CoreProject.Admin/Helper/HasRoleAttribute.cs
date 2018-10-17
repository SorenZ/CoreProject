using System.Net;
using Alamut.Data.Structure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Mobin.CoreProject.Core.SSOT;

namespace Mobin.CoreProject.Admin.Helper
{
    public class HasRoleAttribute : ActionFilterAttribute
    {
        private readonly AuthorityCode _authorityCode;

        public HasRoleAttribute(AuthorityCode authorityCode)
        {
            _authorityCode = authorityCode;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.HasRole(_authorityCode))
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