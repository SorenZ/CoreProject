using Microsoft.AspNetCore.Mvc;

namespace Mobin.CoreProject.Admin.Controllers
{
    public class LogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
