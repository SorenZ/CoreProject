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
using Mobin.CoreProject.AuthAdmin.Models;

namespace Mobin.CoreProject.AuthAdmin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public HomeController(UserManager<AppUser> userManager, 
            RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Setup()
        {
            var user = await _userManager.GetUserAsync(User);

            var adminRole = await _roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                adminRole = new AppRole("Admin");
                await _roleManager.CreateAsync(adminRole);

                await _roleManager.AddClaimAsync(adminRole, new Claim("Permission", "projects.view"));
                await _roleManager.AddClaimAsync(adminRole, new Claim("Permission", "projects.create"));
                await _roleManager.AddClaimAsync(adminRole, new Claim("Permission", "projects.update"));
            }

            if (!await _userManager.IsInRoleAsync(user, adminRole.Name))
            {
                await _userManager.AddToRoleAsync(user, adminRole.Name);
            }

            var accountManagerRole = await _roleManager.FindByNameAsync("Account Manager");

            if (accountManagerRole == null)
            {
                accountManagerRole = new AppRole("Account Manager");
                await _roleManager.CreateAsync(accountManagerRole);

                await _roleManager.AddClaimAsync(accountManagerRole, new Claim("Permission", "account.manage"));
            }

            if (!await _userManager.IsInRoleAsync(user, accountManagerRole.Name))
            {
                await _userManager.AddToRoleAsync(user, accountManagerRole.Name);
            }

            return Ok();
        }

        public IActionResult GetClaims()
        {
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToArray();
            return Json(claims);
        }
    }
}
