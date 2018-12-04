using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mobin.CoreProject.Admin.Extensions;
using Mobin.CoreProject.Admin.Helper;
using Mobin.CoreProject.Core.Helpers;
using Mobin.CoreProject.Core.SSOT;
using Mobin.CoreProject.CrossCutting.Security.Helper;
using Mobin.CoreProject.CrossCutting.Security.Services;

namespace Mobin.CoreProject.Admin.Controllers
{
    // [HasPermission(Permissions.UserAndRoles)]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            var model = _roleService.GetAll();
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
            var result = await _roleService.CreateAsync(name);

            TempData.AddResult(result);


            return result.Succeed
                ? RedirectToAction(nameof(Index), new { id = result.Data })
                : RedirectToAction(nameof(Create));
        }
        #endregion





        #region UpdateRoleTitle
        public async Task<IActionResult> EditName(int id)
        {
            var role = await _roleService.FindByIdAsync(id);
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditName(int id, string title, bool isAjax = false)
        {
            var result = await _roleService.UpdateAsync(id, title);

            if (isAjax) return Json(result);

            TempData.AddResult(result);
            return RedirectToAction(nameof(EditName));
        }
        #endregion


        #region UpdateRoleClaims
        public async Task<IActionResult> EditClaims(int id)
        {
            var role = await _roleService.FindByIdAsync(id);
            var roleClaims = await _roleService.GetClaimsAsync(role);
            var roleClaimsStringArray = roleClaims.Select(c => c.Value).ToList();

            var claims = new Dictionary<string, string>();
            var allClaims = EnumHelper.EnumToList(typeof(Permissions));
            // return Json(allClaims);

            ViewBag.RoleClaimsStringArray = roleClaimsStringArray;
            ViewBag.AllClaims = allClaims;

            return View(role);
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

            var result = await _roleService.UpdatePermissions(id, claims);

            return Json(result);

        }
        #endregion


        #region DeleteRole
        public async Task<IActionResult> Delete(int id, bool isAjax = false)
        {
            var result = await _roleService.DeleteAsync(id);

            if (isAjax) return Json(result);

            TempData.AddResult(result);
            return RedirectToAction(nameof(Index));
        }
        #endregion




        /*
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
        */

    }
}
