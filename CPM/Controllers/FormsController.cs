using Microsoft.AspNetCore.Mvc;

namespace CPM_Project.Controllers
{
    public class FormsController : Controller
    {

        public IActionResult FormsElements()
        {
            return View();
        }

        public IActionResult FormsExtended()
        {
            return View();
        }

        public IActionResult TextEditor()
        {
            return View();
        }

        public IActionResult Wizard()
        {
            return View();
        }

        public IActionResult Validation()
        {
            return View();
        }
    }
}