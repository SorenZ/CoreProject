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
        private readonly IBlogService _blogService;
        private readonly ICrudService<Post> _postService;

        public HomeController(IBlogService blogService, ICrudService<Post> postService)
        {
            _blogService = blogService;
            _postService = postService;
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

        public List<Blog> Blogs()
        {
            return _blogService
                .ReadOnlyRepository
                .GetAll();
        }

        public List<Post> Posts()
        {
            return _postService
                .ReadOnlyRepository
                .GetAll();
        }
    }
}
