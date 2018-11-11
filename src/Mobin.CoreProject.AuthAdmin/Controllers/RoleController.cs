using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mobin.CoreProject.AuthAdmin.Models;

namespace Mobin.CoreProject.AuthAdmin.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        #region CreateRole
        public IActionResult CreateRole()
        {
            var data = new { title = "مدیر واحد تضمین کیفیت" };
            return RedirectToAction(nameof(CreateRolePost), data);
        }

        public IActionResult CreateRolePost(string title)
        {
            // TODO : Create a role
            return Json(title);
        }
        #endregion


        public IActionResult DeleteRole()
        {
            var data = new { id = 1 };
            return RedirectToAction(nameof(DeleteRolePost), data);
        }

        public IActionResult DeleteRolePost(int id)
        {
            // TODO: delete role
            // [ ] assigned to the users (AspNetUserRoles)
            // [ ] delete role claims (AspNetRoleClaims)
            // [ ] the role itself (AspNetRoles)
            return Json(id);
        }

    }
}
