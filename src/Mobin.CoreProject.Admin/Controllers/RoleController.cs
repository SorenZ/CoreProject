using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mobin.CoreProject.Admin.Extensions;
using Mobin.CoreProject.Admin.Helper;
using Mobin.CoreProject.Core.SSOT;
using Mobin.CoreProject.CrossCutting.Security.Helper;
using Mobin.CoreProject.CrossCutting.Security.Services;

namespace Mobin.CoreProject.Admin.Controllers
{
    // [HasPermission(Permissions.UserAndRoles)]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleServices;

        public RoleController(IRoleService roleServices)
        {
            _roleServices = roleServices;
        }

        public IActionResult Index()
        {
            var model = _roleServices.GetAll();
            return View(model);
        }

        #region CreateRole
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name)
        {
            var result = await _roleServices.CreateAsync(name);

            TempData.AddResult(result);


            return result.Succeed
                ? RedirectToAction(nameof(Index), new { id = result.Data })
                : RedirectToAction(nameof(Create));
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
        public async Task<IActionResult> Delete(int id, bool isAjax = false)
        {
            var result = await _roleServices.DeleteAsync(id);

            if (isAjax) return Json(result);

            TempData.AddResult(result);
            return RedirectToAction(nameof(Index));
        }
        #endregion




        // implement HasPermission method
        [HasPermission(Permissions.ForestCreate, Permissions.ForestDelete, Permissions.ForestEdit)]
        public IActionResult HasPermission()
        {
            return Json($"the user has permission {Permissions.ForestCreate}");
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
