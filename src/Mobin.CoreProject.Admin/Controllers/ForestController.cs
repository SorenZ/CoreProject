﻿using System.Linq;
using Alamut.Data.Linq;
using Alamut.Data.Paging;
using Microsoft.AspNetCore.Mvc;
using Mobin.CoreProject.Core.SearchCriteria.Forest;
using Mobin.CoreProject.Core.ServiceContracts;
using Mobin.CoreProject.Core.ViewModels.Forest;

namespace Mobin.CoreProject.Admin.Controllers
{
    public class ForestController : Controller
    {
        private readonly IForestService _forestService;

        public ForestController(IForestService forestService)
        {
            _forestService = forestService;
        }

        public IActionResult Index(ForestGetDataSC criteria, int page = 1)
        {
            var model = _forestService.GetData(criteria, page);

            return View(model);
        }

        #region Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ForestCreateVM vm)
        {
            var result = _forestService.Create(vm);
            return Json(result);

        }
        #endregion
    }


}
