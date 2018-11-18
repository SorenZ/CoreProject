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
        private readonly UserManager<AppUser> _userManager;
        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        #region CreateUser
        public IActionResult CreateUser()
        {
            // add other parameters if they are mandatory
            var data = new { id = 12548 };
            return RedirectToAction(nameof(CreateUserPost), data);
        }

        public async Task<IActionResult> CreateUserPost(int id)
        {
            return Json(new { id });
        }
        #endregion

        #region CreateUser
        public IActionResult DeleteUser()
        {
            var data = new { id = 12548 };
            return RedirectToAction(nameof(DeleteUserPost), data);
        }

        public async Task<IActionResult> DeleteUserPost(int id)
        {
            return Json(new { id });
        }
        #endregion


        
    }
}
