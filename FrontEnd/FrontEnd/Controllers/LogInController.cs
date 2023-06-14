using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class LogInController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
