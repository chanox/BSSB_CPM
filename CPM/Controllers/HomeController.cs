using Microsoft.AspNetCore.Mvc;

namespace CPM_Project.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Error()
        {
            return View();
        }
    }
}
