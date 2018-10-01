using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mobin.CoreProject.Core.Entities;
using Mobin.CoreProject.Core.ServiceContracts;
using Mobin.CoreProject.Core.ViewModels;

namespace Mobin.CoreProject.Admin.Controllers
{
    public class ForestController : Controller
    {
        private readonly IForestService _forestService;

        public ForestController(IForestService forestService)
        {
            _forestService = forestService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Create
        /*
        public IActionResult Create()
        {
            return View();
        }
        */

        public IActionResult Create(string title)
        {
            var result = _forestService.Create(new ForestCreateViewModel { Title = title });
            return Json(result);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ForestCreateViewModel vm)
        {
            var result = _forestService.Create(vm);
            return Json(result);

        }
        #endregion
    }

    
}
