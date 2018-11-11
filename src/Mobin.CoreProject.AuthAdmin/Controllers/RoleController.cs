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
            var data = new { title = "مدیر واحد کنترل کیفیت" };
            return RedirectToAction(nameof(CreateRolePost), data);
        }

        public IActionResult CreateRolePost(string title)
        {
            // TODO : Create a role
            return Json(title);
        }
        #endregion


        #region DeleteRole
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
        #endregion


        #region UpdateRoleTitle
        public IActionResult UpdateRoleTitle()
        {
            var data = new { id = 1, title = "مدیر واحد تضمین کیفیت" };
            return RedirectToAction(nameof(UpdateRoleTitlePost), data);
        }

        public IActionResult UpdateRoleTitlePost(int id, string title)
        {
            // TODO: update role title
            return Json(new { id, title });
        }
        #endregion


        #region UpdateRoleClaims
        public IActionResult UpdateRoleClaims()
        {
            var data = new
            {
                id = 1,
                claims = new List<string>
                {
                    "Forest.Create",
                    "Forest.Delete",
                    "Forest.Manage",
                }
            };

            return RedirectToAction(nameof(UpdateRoleClaimsPost), data);
        }

        public IActionResult UpdateRoleClaimsPost(int id, List<string> claims)
        {
            // TODO: update role claims
            return Json(new { id, claims });
        }
        #endregion

    }
}
