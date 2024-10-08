using Microsoft.AspNetCore.Mvc;

namespace CPM_Project.Controllers
{
    public class LandingPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}