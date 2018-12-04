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
using Mobin.CoreProject.CrossCutting.Security.Services;

namespace Mobin.CoreProject.AuthAdmin.Controllers
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
            var result = await _roleServices.CreateAsync(title);

            return Json(result);
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
            var result = await _roleServices.UpdateAsync(id, title);

            return Json(result);
        }
        #endregion


        #region UpdateRoleClaims
        

        public async Task<IActionResult> UpdateRoleClaimsPost(int id, List<string> claims)
        {
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
            var result = await _roleServices.DeleteAsync(id);

            return Json(result);
        }
        #endregion

        


        /*
        // implement HasPermission method
        [HasPermission(Permissions.ForestCreate)]
        public IActionResult HasPermission()
        {
            return Json($"the user has permission {Permissions.ForestCreate}");
        }
        */
    }
}
