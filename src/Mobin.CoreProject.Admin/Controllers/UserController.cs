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

        public UserController(IUserService userService)
        {
            _userService = userService;
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

        #region UpdateUserRoles
        public IActionResult UpdateUserRoles()
        {
            var data = new
            {
                userId = 1, // userid
                roleIds = new List<int> {6}/*{ 1, 2, 3 }*/,
            };

            return RedirectToAction(nameof(UpdateUserRolesPost), data);
        }

        public async Task<IActionResult> UpdateUserRolesPost(int userId, List<int> roleIds)
        {
            // update user roles
            //var user = await _userManager.FindByIdAsync(userId.ToString());
            //var userRoles = await _userManager.GetRolesAsync(user);
            //var roles = _roleManager.Roles
            //    .Where(q => roleIds.Contains(q.Id))
            //    .Select(s => s.Name);

            //var result = await _userManager.RemoveFromRolesAsync(user, userRoles);

            //if (!result.Succeeded)
            //{return Json(result);}

            //result = await _userManager.AddToRolesAsync(user, roles);
            var result = await _userService.UpdateRoles(userId, roleIds);

            return Json(result);

        }
        #endregion

        #region Delete User
        public IActionResult DeleteUser()
        {
            return RedirectToAction(nameof(DeleteUserPost), new {userName = "m.dashtinejad"});
        }

        public async Task<IActionResult> DeleteUserPost(string userName)
        {
            //var user = await _userManager.FindByNameAsync($"{DomainName}\\{userName}");
            //var result = await _userManager.DeleteAsync(user);

            var result = await _userService.DeleteAsync(userName);

            return Json(result);
        }
        #endregion
        
    }
}
