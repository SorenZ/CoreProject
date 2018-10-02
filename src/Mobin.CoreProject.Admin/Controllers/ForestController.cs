﻿using System.Linq;
using Alamut.Data.Linq;
using Alamut.Data.Paging;
using Alamut.Data.Structure;
using Microsoft.AspNetCore.Mvc;
using Mobin.CoreProject.Admin.Extensions;
using Mobin.CoreProject.Core.DTOs.Forest;
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
            TempData.AddResult(result);
            return RedirectToAction(nameof(Edit), new { id = result.Data });
        }
        #endregion



        #region Edit
        public IActionResult Edit(int id)
        {
            var model = _forestService.Get<ForestEditDTO>(id);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ForestEditVM vm)
        {
            var result = _forestService.Update(id, vm);
            TempData.AddResult(result);
            return RedirectToAction(nameof(Edit), new { id });
        }
        #endregion


        #region Delete
        public IActionResult Delete(int id, bool isAjax = false)
        {
            // var result = _forestService.Delete(id);
            var result = ServiceResult.Okay();

            if (isAjax) return Json(result);

            TempData.AddResult(result);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }


}
