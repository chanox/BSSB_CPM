using Microsoft.AspNetCore.Mvc;

namespace CPM_Project.Controllers
{
    public class WidgetsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}