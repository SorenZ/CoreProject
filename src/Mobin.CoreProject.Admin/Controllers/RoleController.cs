﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mobin.CoreProject.Admin.Helper;
using Mobin.CoreProject.Core.SSOT;
using Mobin.CoreProject.CrossCutting.Security.Helper;
using Mobin.CoreProject.CrossCutting.Security.Services;

namespace Mobin.CoreProject.Admin.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleServices;
        
        public RoleController(IRoleService roleServices)
        {
            _roleServices = roleServices;
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
            //var newRole = await _roleManager.FindByNameAsync(title);
            //if (newRole == null)
            //{
            //    newRole = new AppRole(title);
            //    var result = await _roleManager.CreateAsync(newRole);
            //    return Json(result);
            //}

            var result = await _roleServices.CreateAsync(title);

            return Json(result);
        }
        #endregion


        


        #region UpdateRoleTitle
        public IActionResult UpdateRoleTitle()
        {
            var data = new { id = 1, title = "مدیر" };
            return RedirectToAction(nameof(UpdateRoleTitlePost), data);
        }

        public async Task<IActionResult> UpdateRoleTitlePost(int id, string title)
        {
            ////  update role title
            //var role = await _roleManager.FindByIdAsync(id.ToString());
            //role.Name = title;

            //var result = await _roleManager.UpdateAsync(role);

            var result = await _roleServices.UpdateAsync(id, title);

            return Json(result);
        }
        #endregion


        #region UpdateRoleClaims
        public IActionResult UpdateRoleClaims()
        {
            var data = new
            {
                id = 6,
                claims = new List<string>
                {
                    Permissions.ForestCreate.ToString(),
                    Permissions.ForestDelete.ToString(),
                    Permissions.ForestEdit.ToString()
                }
            };

            return RedirectToAction(nameof(UpdateRoleClaimsPost), data);
        }

        public async Task<IActionResult> UpdateRoleClaimsPost(int id, List<string> claims)
        {
            // update role claims
            //var role = await _roleManager.FindByIdAsync(id.ToString());

            //if (role == null)
            //    {return Content($"there is no role with Id : {id}");}

            //var currentClaims = await _roleManager.GetClaimsAsync(role);

            //foreach (var claim in claims)
            //{
            //    if (currentClaims.Any(q => q.Value == claim)) // simplified 
            //        { continue;}

            //    await _roleManager.AddClaimAsync(role, new Claim(AlamutClaimTypes.Permission, claim));
            //}
            
            //return Json(new { id, claims });

            var result = await _roleServices.UpdatePermissions(id, claims);

            return Json(result);

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
            //var role = _roleManager.Roles.FirstOrDefault(q => q.Id == id);
            //var result = await _roleManager.DeleteAsync(role);

            var result = await _roleServices.DeleteAsync(id);

            return Json(result);
        }
        #endregion

        


        // implement HasPermission method
        [HasPermission(Permissions.ForestCreate, Permissions.ForestDelete, Permissions.ForestEdit)]
        public IActionResult HasPermission()
        {
            return Json($"the user has permission {Permissions.ForestCreate}");
        }


        [HasPermission(Permissions.NoPermission)]
        public IActionResult NoPermission()
        {
            return Json($"the user has permission {Permissions.NoPermission}");
        }

        public IActionResult GetClaims()
        {
            var keyPair = User.Claims.Select(s => new
            {
                s.Type,
                s.Value
            });

            return Json(keyPair);
        }

        public IActionResult GetUserInfo()
        {
            return Json(new
            {
                Id = User.GetUserId(),
                Name = User.Identity.Name
            });
        }
        

    }
}
