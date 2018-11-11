using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mobin.CoreProject.AuthAdmin.Areas.Identity;
using Mobin.CoreProject.AuthAdmin.Models;

namespace Mobin.CoreProject.AuthAdmin.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {

        private readonly RoleManager<AppRole> _roleManager;

        public RoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        #region CreateRole
        public IActionResult CreateRole()
        {
            var data = new { title = "مدیر واحد کنترل کیفیت" };
            return RedirectToAction(nameof(CreateRolePost), data);
        }

        public async Task<IActionResult> CreateRolePost(string title)
        {
            // Create a role
            var newRole = await _roleManager.FindByNameAsync(title);
            if (newRole == null)
            {
                newRole = new AppRole(title);
                var result = await _roleManager.CreateAsync(newRole);
                return Json(result);
            }

            return Json($"The role {title} already exist.");
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

        #region UpdateUserRoles
        public IActionResult UpdateUserRoles()
        {
            var data = new
            {
                userId = 1, // userid
                roleIds = new List<int> { 1, 2, 3 },
            };

            return RedirectToAction(nameof(UpdateUserRolesPost), data);
        }

        public IActionResult UpdateUserRolesPost(int userId, List<int> roleIds)
        {
            // TODO: update user roles
            // [ ] delete all user roles
            // [ ] assign these roles to the user
            return Json(new { userId, roleIds });
        }
        #endregion

    }
}
