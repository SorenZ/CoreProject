using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mobin.CoreProject.Admin.Helper;
using Mobin.CoreProject.Core.SSOT;

namespace Mobin.CoreProject.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HasRole(AuthorityCode.SystemSupervisor)]
        public IActionResult SystemSupervisor() => 
            Content(AuthorityCode.SystemSupervisor.ToString());

        [HasRole(AuthorityCode.UserManagementFullAccess)]
        public IActionResult UserManagementFullAccess() => 
            Content(AuthorityCode.UserManagementFullAccess.ToString());

    }
}
