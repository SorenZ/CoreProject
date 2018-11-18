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
using Mobin.CoreProject.AuthAdmin.Helper;
using Mobin.CoreProject.AuthAdmin.Models;
using Mobin.CoreProject.Core.SSOT;

namespace Mobin.CoreProject.AuthAdmin.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {

        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
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

        public async Task<IActionResult> UpdateRoleTitlePost(int id, string title)
        {
            //  update role title
            var role = await _roleManager.FindByIdAsync(id.ToString());
            role.Name = title;

            var result = await _roleManager.UpdateAsync(role);

            return Json(result);
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

        public async Task<IActionResult> UpdateRoleClaimsPost(int id, List<string> claims)
        {
            // update role claims
            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role == null)
                {return Content($"there is no role with Id : {id}");}

            var currentClaims = await _roleManager.GetClaimsAsync(role);

            foreach (var claim in claims)
            {
                if (currentClaims.Any(q => q.Value == claim)) // simplified 
                    { continue;}

                await _roleManager.AddClaimAsync(role, new Claim("Permission", claim));
            }
            


            return Json(new { id, claims });
        }
        #endregion


        #region DeleteRole
        public IActionResult DeleteRole()
        {
            var data = new { id = 1 };
            return RedirectToAction(nameof(DeleteRolePost), data);
        }

        public async Task<IActionResult> DeleteRolePost(int id)
        {
            //  delete role
            // [ ] assigned to the users (AspNetUserRoles)
            // [ ] delete role claims (AspNetRoleClaims)
            // [ ] the role itself (AspNetRoles)
            var role = _roleManager.Roles.FirstOrDefault(q => q.Id == id);
            var result = await _roleManager.DeleteAsync(role);

            return Json(result);
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

        public async Task<IActionResult> UpdateUserRolesPost(int userId, List<int> roleIds)
        {
            // update user roles
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = _roleManager.Roles
                .Where(q => roleIds.Contains(q.Id))
                .Select(s => s.Name);

            var result = await _userManager.RemoveFromRolesAsync(user, userRoles);

            if (!result.Succeeded)
                {return Json(result);}

            result = await _userManager.AddToRolesAsync(user, roles);

            return Json(result);

        }
        #endregion


        // TODO: implement HasPermission method
        [HasPermission(Permissions.ForestCreate)]
        public IActionResult HasPermission()
        {
            return Json($"the user has permission {Permissions.ForestCreate}");
        }
    }
}
