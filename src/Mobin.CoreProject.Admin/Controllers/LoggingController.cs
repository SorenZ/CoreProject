using Microsoft.AspNetCore.Mvc;

namespace Mobin.CoreProject.Admin.Controllers
{
    public class LoggingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
