using Microsoft.AspNetCore.Mvc;

namespace CPM_Project.Controllers
{
    public class AnalyticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}