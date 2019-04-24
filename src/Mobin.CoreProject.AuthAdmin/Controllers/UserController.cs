using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mobin.CoreProject.AuthAdmin.Extensions;
using Mobin.CoreProject.Core.Helpers;
using Mobin.CoreProject.Core.SSOT;
using Mobin.CoreProject.CrossCutting.Security.Models;
using Mobin.CoreProject.CrossCutting.Security.Services;

namespace Mobin.CoreProject.AuthAdmin.Controllers
{

    [AllowAnonymous]
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
            var result = await _userService.CreateDomainUserAsync(userName);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUsername(int id, string newUsername)
        {
            var result = await _userService.RenameUsername(id, newUsername);
            TempData.AddResult(result);
            return RedirectToAction(nameof(Index));
        }

        #endregion


        #region UpdateUserRoles
        public async Task<IActionResult> EditRoles(int id)
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
        public async Task<IActionResult> EditRoles(int id, List<int> roleIds, bool isAjax)
        {
            var result = await _userService.UpdateRoles(id, roleIds);

            if (isAjax) return Json(result);

            TempData.AddResult(result);
            return RedirectToAction(nameof(EditRoles), new { id });

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


        #region UpdateClaims
        public async Task<IActionResult> EditClaims(int id)
        {
            var user = await _userService.FindByIdAsync(id);
            var userClaims = await _userService.GetClaimsAsync(id);
            var allClaims = EnumHelper.EnumToList(typeof(Claims));

            var model = new UserEditClaimsPM
            {
                User = user,
                UserClaims = userClaims,
                AllClaims = allClaims,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditClaims(int id, string claimType, string claimValue)
        {
            var result = await _userService.SetClaimAsync(id, claimType, claimValue);

            TempData.AddResult(result);
            return RedirectToAction(nameof(EditClaims), new { id });

        }

        public async Task<IActionResult> DeleteSingleClaim(int id, string claimType, string claimValue, bool isAjax = false)
        {
            var result = await _userService.RemoveClaimAsync(id, claimType, claimValue);

            if (isAjax) return Json(result);

            TempData.AddResult(result);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region CreatePublicUser

        public IActionResult CreatePublicUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePublicUser(string userName, string password)
        {
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: null,
                protocol: Request.Scheme);
            

            var result = await _userService.CreatePublicUserAsync(userName, password,
                userName, // optional
                callbackUrl); // optional

            TempData.AddResult(result);

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region EditPublicUsername
        public async Task<IActionResult> EditPublicUsername(int id)
        {
            var user = await _userService.FindByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPublicUsername(int id, string newUsername, string newPassword)
        {
            var result = await _userService.RenameUsername(id, newUsername);
            TempData.AddResult(result);
            return RedirectToAction(nameof(Index));
        }
        #endregion

    }


    public class UserEditClaimsPM
    {
        public AppUser User { get; set; }
        public IList<Claim> UserClaims { get; set; }
        public List<EnumHelper.EnumModel> AllClaims { get; set; }
    }
}
