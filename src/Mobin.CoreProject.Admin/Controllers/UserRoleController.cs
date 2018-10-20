using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mobin.CoreProject.Admin.Extensions;
using Mobin.CoreProject.Core.ServiceContracts;
using Mobin.CoreProject.Core.ViewModels.UserRole;

namespace Mobin.CoreProject.Admin.Controllers
{
    [Authorize]
    public class UserRoleController : Controller
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        public IActionResult Index(int page = 1, int size = 10)
        {
            var model = _userRoleService.GetData(page, size);

            return View(model);
        }


        #region Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserRoleCreateVM vm)
        {
            var result = _userRoleService.Create(vm);
            TempData.AddResult(result);
            return RedirectToAction(nameof(Index));
        }
        #endregion



        

        #region Delete
        public IActionResult Delete(string userName, string roleName, bool isAjax = false)
        {
            var result = _userRoleService.Delete(userName, roleName);

            if (isAjax) return Json(result);

            TempData.AddResult(result);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }


}
