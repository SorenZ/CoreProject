﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Mobin.CoreProject.Admin.Extensions;
using Mobin.CoreProject.Core.DTOs.Forest;
using Mobin.CoreProject.Core.SearchCriteria.Forest;
using Mobin.CoreProject.Core.ServiceContracts;
using Mobin.CoreProject.Core.SSOT;
using Mobin.CoreProject.Core.ViewModels.Forest;
using Mobin.CoreProject.CrossCutting.Security.Helper;

namespace Mobin.CoreProject.Admin.Controllers
{
    public class ForestController : Controller
    {
        private readonly IForestService _forestService;

        public ForestController(IForestService forestService)
        {
            _forestService = forestService;
        }

        public IActionResult Index(ForestGetDataSC criteria, int page = 1, int size = 10)
        {
            var model = _forestService.GetData(criteria, page, size);
            ViewBag.Q = criteria.Q;

            return View(model);
        }

        public IActionResult GetClaims()
        {
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToArray();
            return Json(claims);
        }

        public IActionResult GetEmployeeId()
        {
            // var employeeNumber = User.FindFirstValue("EmployeeNumber");
            var employeeNumber = User.GetClaim(Claims.EmployeeNumber.ToString());
            return Json(employeeNumber);
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
        public IActionResult Edit(int id, ForestEditVM vm, bool isAjax = false)
        {
            var result = _forestService.Update(id, vm);

            if (isAjax) return Json(result);

            TempData.AddResult(result);
            return RedirectToAction(nameof(Edit), new { id });
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
