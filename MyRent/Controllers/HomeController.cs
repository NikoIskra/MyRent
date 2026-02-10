using Microsoft.AspNetCore.Mvc;

namespace MyRent.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
             return View();
        }
    }
}
