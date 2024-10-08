using Microsoft.AspNetCore.Mvc;

namespace CPM_Project.Controllers
{
    public class OptionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}