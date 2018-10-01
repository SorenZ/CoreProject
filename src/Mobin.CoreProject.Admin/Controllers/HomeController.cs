using Microsoft.AspNetCore.Mvc;

namespace Mobin.CoreProject.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
