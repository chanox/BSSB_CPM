using Microsoft.AspNetCore.Mvc;

namespace CPM_Project.Controllers
{
    public class GridSystemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}