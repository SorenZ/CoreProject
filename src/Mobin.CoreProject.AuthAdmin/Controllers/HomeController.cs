using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Mobin.CoreProject.AuthAdmin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // return Json(User.Claims.ToList());
            return View();
        }

        

    }
}
