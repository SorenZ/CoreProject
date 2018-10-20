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
        private readonly IUserService _forestService;

        public UserController(IUserService forestService
            , ILeafService leafService)
        {
            _forestService = forestService;
        }

        public IActionResult Index(int page = 1, int size = 10)
        {
            var model = _forestService.GetData(page, size);

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
            var result = _forestService.Create(vm);
            TempData.AddResult(result);
            return RedirectToAction(nameof(Index));
        }
        #endregion



        

        #region Delete
        public IActionResult Delete(int id, bool isAjax = false)
        {
            var result = _forestService.Delete(id);

            if (isAjax) return Json(result);

            TempData.AddResult(result);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }


}
