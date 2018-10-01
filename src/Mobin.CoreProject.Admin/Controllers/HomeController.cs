using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Alamut.Data.Service;
using Microsoft.AspNetCore.Mvc;
using Mobin.CoreProject.Admin.Models;
using Mobin.CoreProject.Core.Entities;
using Mobin.CoreProject.Core.ServiceContracts;

namespace Mobin.CoreProject.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IForestService _forestService;

        public HomeController(IForestService forestService)
        {
            _forestService = forestService;
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

        public List<Forest> Forests()
        {
            return _forestService
                .ReadOnlyRepository
                .GetAll();
        }
    }
}
