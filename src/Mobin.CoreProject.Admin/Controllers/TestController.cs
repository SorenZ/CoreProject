using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobin.CoreProject.Core.SSOT;
using Mobin.CoreProject.CrossCutting.Security.Helper;

namespace Mobin.CoreProject.Admin.Controllers
{
    public class TestController : Controller
    {
        public IActionResult FileUpload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FileUpload(string file)
        {
            return Content(file);
        }


        public IActionResult GetClaim()
        {
            var claim = User.GetClaimInt(Claims.EmployeeId);
            return Json(new { claim });
        }
    }
}
