using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mobin.CoreProject.Admin.Extensions;
using Mobin.CoreProject.Core.ServiceContracts;
using Mobin.CoreProject.Core.ViewModels.User;

namespace Mobin.CoreProject.Admin.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService
            , ILeafService leafService)
        {
            _userService = userService;
        }

        public IActionResult Index(int page = 1, int size = 10)
        {
            var model = _userService.GetData(page, size);

            return View(model);
        }


        #region Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserCreateVM vm)
        {
            var result = _userService.Create(vm);
            TempData.AddResult(result);
            return RedirectToAction(nameof(Index));
        }
        #endregion



        

        #region Delete
        public IActionResult Delete(string userName, bool isAjax = false)
        {
            var result = _userService.Delete(userName);

            if (isAjax) return Json(result);

            TempData.AddResult(result);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var model = _userService.GetAll();
            return Json(model);
        }
    }


}
