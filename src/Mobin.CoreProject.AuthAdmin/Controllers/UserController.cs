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
    public class UserController : Controller
    {
        private const string DomainName = "MOBINNET";
        private const string DomainEMail = "mobinnet.net";
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        #region CreateUser
        public IActionResult CreateUser()
        {
            return RedirectToAction(nameof(CreateUserPost), new {userName = "m.dashtinejad"});

            // add other parameters if they are mandatory
            //var data = new { id = 12548 };
            


        }

        public async Task<IActionResult> CreateUserPost(string userName)
        {
            var user = new AppUser {UserName = $"{DomainName}\\{userName}", Email = $"{userName}@{DomainEMail}"};
            var result = await _userManager.CreateAsync(user);

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
            var user = await _userManager.FindByNameAsync($"{DomainName}\\{userName}");
            var result = await _userManager.DeleteAsync(user);

            return Json(result);
        }
        #endregion


        
    }
}
