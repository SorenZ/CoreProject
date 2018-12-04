using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mobin.CoreProject.Admin.Extensions;
using Mobin.CoreProject.CrossCutting.Security.Services;

namespace Mobin.CoreProject.Admin.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            var model = _userService.GetAll();
            return View(model);
        }

        #region CreateUser
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string userName)
        {
            var result = await _userService.CreateAsync(userName);
            TempData.AddResult(result);
            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region EditUsername
        public async Task<IActionResult> EditUsername(int id)
        {
            var user = await _userService.FindByIdAsync(id);
            return View(user);
        }
        #endregion


        #region UpdateUserRoles
        public async Task<IActionResult> Roles(int id)
        {
            var user = await _userService.FindByIdAsync(id);
            var roles = _roleService.GetAll();
            var userRoles = await _userService.GetRolesAsync(user);

            ViewBag.UserRoles = userRoles;
            ViewBag.User = user;

            return View(roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Roles(int id, List<int> roleIds, bool isAjax)
        {
            var result = await _userService.UpdateRoles(id, roleIds);

            if (isAjax) return Json(result);

            TempData.AddResult(result);
            return RedirectToAction(nameof(Roles), new { id });

        }
        #endregion

        #region Delete User
        public async Task<IActionResult> Delete(int id, bool isAjax = false)
        {
            var result = await _userService.DeleteAsync(id);

            if (isAjax) return Json(result);

            TempData.AddResult(result);
            return RedirectToAction(nameof(Index));
        }
        #endregion
        
    }
}
