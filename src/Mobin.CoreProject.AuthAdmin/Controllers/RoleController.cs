using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mobin.CoreProject.AuthAdmin.Extensions;
using Mobin.CoreProject.Core.Helpers;
using Mobin.CoreProject.Core.SSOT;
using Mobin.CoreProject.CrossCutting.Security.Services;

namespace Mobin.CoreProject.AuthAdmin.Controllers
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
                ? RedirectToAction(nameof(EditClaims), new { id = result.Data })
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

            var allClaims = EnumHelper.EnumToList(typeof(Permissions));

            ViewBag.RoleClaimsStringArray = roleClaimsStringArray;
            ViewBag.AllClaims = allClaims;

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditClaims(int id, List<string> claims, bool isAjax = false)
        {
            var result = await _roleService.UpdatePermissions(id, claims);

            if (isAjax) return Json(result);

            TempData.AddResult(result);
            return RedirectToAction(nameof(EditClaims), new { id });

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


    }
}
